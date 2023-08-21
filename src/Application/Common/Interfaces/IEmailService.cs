namespace SiMinor7.Application.Common.Interfaces;

public interface IEmailService : IScopedService
{
    void SendEmail(string email, string subject, string templateName, object data);
}