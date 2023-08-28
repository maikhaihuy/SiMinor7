using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SiMinor7.Apis.Controllers;
using SiMinor7.Application.Auth.Commands.ForgotPassword;
using SiMinor7.Application.Auth.Commands.Login;
using SiMinor7.Application.Auth.Commands.RefreshToken;
using SiMinor7.Application.Auth.Commands.UpdatePassword;
using SiMinor7.Application.Auth.Shared.Models;

namespace Apis.Controllers;

public class AuthController : ApiControllerBase
{
    [HttpPost("login")]
    public async Task<ActionResult<AuthResponse>> Login(LoginCommand loginCommand)
    {
        return await Mediator.Send(loginCommand);
    }

    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPassword(ForgotPasswordCommand forgotPasswordCommand)
    {
        await Mediator.Send(forgotPasswordCommand);
        return NoContent();
    }

    [HttpPost("update-password")]
    public async Task<IActionResult> UpdatePassword(UpdatePasswordCommand updatePasswordCommand)
    {
        await Mediator.Send(updatePasswordCommand);
        return NoContent();
    }

    [HttpPost("refresh-token")]
    public async Task<ActionResult<AuthResponse>> RefreshToken(RefreshTokenCommand command)
    {
        return await Mediator.Send(command);
    }
}