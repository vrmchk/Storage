namespace Storage.Common.Models.Configs;

public class EmailConfig
{
    public string SmtpServer { get; set; } = string.Empty;
    public int SmtpPort { get; set; }
    public string DefaultEmail { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string TemplatesPath { get; set; } = string.Empty;
}