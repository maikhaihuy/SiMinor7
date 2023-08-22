
namespace SiMinor7.Application.Common.Constants;

public class App
{
    public const int StartPageNumber = 1;
    public const int PageSize = 10;

    public static class ResponseCodeMessage
    {
        public const string AccountNotExists = "messages.accountNotExists";
        public const string AccountActive = "messages.accountActive";
        public const string AccountDeactive = "messages.accountDeactive";
        public const string AccountIncomplete = "messages.accountIncomplete";
        public const string NewPasswordIsSameOldPassword = "messages.newPasswordIsSameOldPassword";
        public const string ValidationFailed = "messages.validationFailed";
        public const string EmailTokenExpired = "messages.emailTokenExpired";
        public const string UserTokenValid = "messages.userTokenValid";
        public const string EmailExisted = "messages.emailExisted";
        public const string RecordNotFound = "messages.recordNotFound";
    }
}