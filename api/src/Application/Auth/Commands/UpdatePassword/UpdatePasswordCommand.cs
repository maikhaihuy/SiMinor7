using System.Text;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using SiMinor7.Application.Common.Exceptions;
using SiMinor7.Application.Common.Constants;
using SiMinor7.Domain.Entities;

namespace SiMinor7.Application.Auth.Commands.UpdatePassword;

public record UpdatePasswordCommand : IRequest
{
    public string Token { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public string Purpose { get; set; } = string.Empty;
}

public class UpdatePasswordCommandHandler : IRequestHandler<UpdatePasswordCommand>
{
    private readonly UserManager<ApplicationUser> _userManager;

    public UpdatePasswordCommandHandler(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task Handle(UpdatePasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.UserId);

        if (user is null)
        {
            throw new NotFoundException($"{App.ResponseCodeMessage.AccountNotExists}. The resource you have requested cannot be found");
        }
        string decodedToken = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(request.Token));

        var isUserTokenValid = await _userManager.VerifyUserTokenAsync(user, TokenOptions.DefaultProvider, request.Purpose, decodedToken);
        if (!isUserTokenValid)
        {
            throw new InvalidRequestException($"{App.ResponseCodeMessage.UserTokenValid}. The link has been expired or invalid.");
        }

        if (request.Purpose == UserManager<ApplicationUser>.ResetPasswordTokenPurpose)
        {
            var checkSamePreviousPassword = await _userManager.CheckPasswordAsync(user, request.Password);
            if (checkSamePreviousPassword)
            {
                throw new InvalidRequestException($"{App.ResponseCodeMessage.NewPasswordIsSameOldPassword}. Your new password cannot be the same as your current password. Please try another password.");
            }

            IdentityResult iResult = await _userManager.ResetPasswordAsync(user, decodedToken, request.Password);
            if (!iResult.Succeeded)
            {
                string[] errors = iResult.Errors.Select(error => $"{error.Code}:{error.Description}").ToArray();
                throw new Exception("The new password could not be assigned." + Environment.NewLine + string.Join(Environment.NewLine, errors));
            }
        }
        else if (request.Purpose == UserManager<ApplicationUser>.ConfirmEmailTokenPurpose)
        {
            IdentityResult iResult = await _userManager.ConfirmEmailAsync(user, decodedToken);
            if (iResult.Succeeded)
            {
                iResult = await _userManager.AddPasswordAsync(user, request.Password);
            }

            if (!iResult.Succeeded)
            {
                string[] errors = iResult.Errors.Select(error => $"{error.Code}:{error.Description}").ToArray();
                throw new Exception("The new password could not be assigned." + Environment.NewLine + string.Join(Environment.NewLine, errors));
            }
        }
    }
}