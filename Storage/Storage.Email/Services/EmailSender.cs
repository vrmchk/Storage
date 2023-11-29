using ErrorOr;
using FluentEmail.Core;
using Storage.Common.Models.Configs;
using Storage.Email.Models.Base;
using Storage.Email.Services.Interfaces;

namespace Storage.Email.Services;

public class EmailSender : IEmailSender
{
    private readonly IFluentEmail _email;
    private readonly EmailConfig _emailConfig;

    public EmailSender(IFluentEmail email, EmailConfig emailConfig)
    {
        _email = email;
        _emailConfig = emailConfig;
    }

    public async Task<ErrorOr<Success>> SendEmailAsync<T>(string to, T message)
        where T : EmailMessageBase
    {
        var path = $@"{_emailConfig.TemplatesPath}\{message.TemplateName}.cshtml";

        var response = await _email
            .To(to)
            .Subject(message.Subject)
            .UsingTemplateFromFile(path, message)
            .SendAsync();

        if (!response.Successful)
            return Error.Failure("Unable to send an email. Please try again later");

        return Result.Success;
    }
}