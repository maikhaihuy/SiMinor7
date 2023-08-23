namespace SiMinor7.Application.Common.Models;

public record EmailDto
{
    public required string Receiver { get; init; }
    public required string Subject { get; init; }
    public required string TemplateName { get; init; }
    public required Type Type { get; init; }
    public required object Data { get; init; }
    public string EmbeddedTemplateFile => $"{Type.Namespace}.{TemplateName}";
}