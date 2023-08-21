using SiMinor7.Domain.Enums;

namespace SiMinor7.Application.Users.Models;

public record UserListDto
{
    public string Id { get; set; } = string.Empty;

    public string Avatar { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string FullName => $"{FirstName} {LastName}";

    public DateTimeOffset DateCreated { get; set; }

    public UserStatus Status { get; set; }

    public IEnumerable<string> Roles { get; set; } = Enumerable.Empty<string>();
}