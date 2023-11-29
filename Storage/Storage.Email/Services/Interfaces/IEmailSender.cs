using ErrorOr;
using Storage.Email.Models.Base;

namespace Storage.Email.Services.Interfaces;

public interface IEmailSender
{
    Task<ErrorOr<Success>> SendEmailAsync<T>(string to, T message) where T : EmailMessageBase;
}