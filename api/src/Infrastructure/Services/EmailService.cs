using System.Reflection;
using FluentEmail.Core;
using FluentEmail.Core.Models;
using Microsoft.Extensions.Logging;
using SiMinor7.Application.Common.Interfaces;
using SiMinor7.Application.Common.Models;

namespace SiMinor7.Infrastructure.Services;

public class EmailService : IEmailService
{
    private readonly IFluentEmail _fluentEmail;
    private readonly ILogger _logger;

    public EmailService(IFluentEmail fluentEmail, ILogger<EmailService> logger)
    {
        _fluentEmail = fluentEmail;
        _logger = logger;
    }

    public void SendEmail(string receiver, string subject, string templateName, object data)
    {
        try
        {
            var type = GetType();
            var embeddedTemplateFile = $"{type.Namespace}.{templateName}";
            var email = _fluentEmail
                        .To(receiver)
                        .Subject(subject)
                        .UsingTemplateFromEmbedded(embeddedTemplateFile, data,
                        Assembly.GetAssembly(type));

            _ = Task.Run(() => email.Send());
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, "An error occured while sending an email.");
        }
    }

    public void SendEmail(EmailDto emailDto)
    {
        try
        {
            var email = _fluentEmail
                        .To(emailDto.Receiver)
            .Subject(emailDto.Subject)
                        .UsingTemplateFromEmbedded(emailDto.EmbeddedTemplateFile, emailDto.Data,
                        Assembly.GetAssembly(emailDto.Type));

            _ = Task.Run(() => email.Send());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occured while sending an email.");
        }
    }
}