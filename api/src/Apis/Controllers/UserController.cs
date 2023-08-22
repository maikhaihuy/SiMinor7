using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using SiMinor7.Apis.Controllers;
using SiMinor7.Application.Common.Models.Paging;
using SiMinor7.Application.Users.Models;
using SiMinor7.Application.Users.Queries.GetUsersWithPagination;
using SiMinor7.Application.Users.Queries.GetUsersList;
using SiMinor7.Application.Users.Queries.GetUserDetail;
using System.Collections.Generic;
using SiMinor7.Application.Users.Commands.SendInvitation;
using SiMinor7.Application.Users.Commands.ResendInvitation;
using SiMinor7.Application.Users.Commands.UpdateStatus;
using SiMinor7.Domain.Enums;

namespace Apis.Controllers;

public class UserController: ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult<PaginatedList<UserListDto>>> GetUsersWithPagination([FromQuery] GetUsersWithPaginationQuery query)
    {
        var result = await Mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("search")]
    public async Task<ActionResult<List<UserListDto>>> GetUsers([FromQuery] GetUserListQuery query)
    {
        var result = await Mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserDetailDto>> GetUserDetail(string id)
    {
        var result = await Mediator.Send(new GetUserDetailQuery(id));
        return Ok(result);
    }

    [HttpPost("invite")]
    public async Task<ActionResult<string>> SendInvitation(SendInvitationCommand command)
    {
        var result = await Mediator.Send(command);
        return result;
    }

    [HttpPut("{id}/re-invite")]
    public async Task<ActionResult<string>> ResendInvitation(ResendInvitationCommand command)
    {
        await Mediator.Send(command);
        return NoContent();
    }

    [HttpPut("{id}/update-status/{status}")]
    public async Task<ActionResult<string>> UpdateStatus(string id, UserStatus status)
    {
        await Mediator.Send(new UpdateStatusCommand(id, status));
        return NoContent();
    }
}