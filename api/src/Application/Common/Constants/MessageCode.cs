using System;
namespace SiMinor7.Application.Common.Constants;

public static class MessageCode
{
    public const string AccountNotExists = "messages.accountNotExists";
    public const string ActiveAccount = "messages.activeAccount";
    public const string InactiveAccount = "messages.inactiveAccount";
    public const string IncompleteAccount = "messages.incompleteAccount";
    public const string BlockedAccount = "messages.blockedAccount";
    public const string InvalidCredential = "messages.invalidCredential";
    public const string ExpiredCredential = "messages.expiredCredential";
    public const string NewPasswordIsSameOldPassword = "messages.newPasswordIsSameOldPassword";
    public const string ValidationFailed = "messages.validationFailed";
    public const string ExpiredToken = "messages.expiredToken";
    public const string InvalidToken = "messages.invalidToken";
    public const string EmailExisted = "messages.emailExisted";
    public const string RecordNotFound = "messages.recordNotFound";
}