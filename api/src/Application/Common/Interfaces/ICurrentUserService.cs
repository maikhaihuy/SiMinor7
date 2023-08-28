namespace SiMinor7.Application.Common.Interfaces;

public interface ICurrentUserService : IScopedService
{
    string? UserId { get; }
    string UserAgent { get; }
}