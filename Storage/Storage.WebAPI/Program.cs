using System.Text;
using FluentEmail.MailKitSmtp;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Storage.BLL.RequestHandlers.Auth;
using Storage.BLL.Validators.Auth;
using Storage.Common.Models.Configs;
using Storage.DAL.Contexts;
using Storage.DAL.Entities;
using Storage.DAL.Repositories;
using Storage.DAL.Repositories.Interfaces;
using Storage.Email.Services;
using Storage.Email.Services.Interfaces;
using Storage.Mapping.WebAPI.Profiles;
using Storage.WebAPI.Extensions;

var builder = WebApplication.CreateBuilder(args);

//Configs
var jwtConfig = new JwtConfig();
builder.Configuration.Bind("Jwt", jwtConfig);
builder.Services.AddSingleton(jwtConfig);

var emailConfig = new EmailConfig();
builder.Configuration.Bind("Email", emailConfig);
emailConfig.TemplatesPath = emailConfig.TemplatesPath.ToAbsolutePath();
builder.Services.AddSingleton(emailConfig);

//Services
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
        .UseLoggerFactory(LoggerFactory.Create(cfg => cfg.AddConsole())));

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<SignUpRequestHandler>());
builder.Services.AddAutoMapper(typeof(AuthProfile));
builder.Services.AddValidatorsFromAssemblyContaining<SignUpRequestValidator>();

//Email
builder.Services.AddFluentEmail(emailConfig.DefaultEmail)
    .AddRazorRenderer(emailConfig.TemplatesPath)
    .AddMailKitSender(new SmtpClientOptions {
        Server = emailConfig.SmtpServer,
        Port = emailConfig.SmtpPort,
        User = emailConfig.DefaultEmail,
        Password = emailConfig.Password,
        UseSsl = false,
        RequiresAuthentication = true,
    });

builder.Services.AddScoped<IEmailSender, EmailSender>();

//Auth
builder.Services.AddIdentity<User, IdentityRole<Guid>>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddTokenProvider<DataProtectorTokenProvider<User>>(TokenOptions.DefaultProvider);

var tokenValidationParameters = new TokenValidationParameters
{
    ValidateIssuerSigningKey = true,
    ValidateIssuer = true,
    ValidateAudience = true,
    ValidateLifetime = true,
    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.Secret)),
    ValidIssuer = jwtConfig.Issuer,
    ValidAudience = jwtConfig.Audience,
    ClockSkew = jwtConfig.ClockSkew
};
builder.Services.AddSingleton(tokenValidationParameters);

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.SaveToken = true;
        options.TokenValidationParameters = tokenValidationParameters;
    });

//Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Storage API", Version = "v1" });

    c.AddSecurityDefinition("Bearer",
        new OpenApiSecurityScheme
        {
            Description = "Standard Authorization header using the Bearer scheme. Example: \"bearer {token}\"",
            In = ParameterLocation.Header,
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey
        });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

await app.SetupRolesAsync();

app.Run();