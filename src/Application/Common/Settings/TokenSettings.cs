namespace SiMinor7.Application.Common.Settings;

public class TokenSettings
{
    public string Key { get; set; } = string.Empty;
    public string Issuer { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
    public double AccessTokenExpiryTimeInHours { get; set; }
    public double AccessTokenExpiryTimeInDays { get; set; }
}