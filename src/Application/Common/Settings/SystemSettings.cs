namespace SiMinor7.Application.Common.Settings;

public class SystemSettings
{
    public string ClientAppBaseUrl { get; set; } = string.Empty;

    public string ClientAppSetPasswordPath { get; set; } = string.Empty;

    public double EmailTokenExpiredTimeInHours { get; set; }

    public string ResetPasswordEmailSubject { get; set; } = string.Empty;

    public string InvitationEmailSubject { get; set; } = string.Empty;

    public string Salt { get; set; } = string.Empty;
}