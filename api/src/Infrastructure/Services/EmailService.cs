using System.Reflection;
using FluentEmail.Core;
using SiMinor7.Application.Common.Interfaces;

namespace SiMinor7.Infrastructure.Services;

public class EmailService : IEmailService
{
    private readonly IFluentEmail _fluentEmail;

    public EmailService(IFluentEmail fluentEmail)
    {
        _fluentEmail = fluentEmail;
    }

    public void SendEmail(string receiver, string subject, string templateName, object data)
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
}