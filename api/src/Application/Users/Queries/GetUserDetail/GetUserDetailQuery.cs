using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using SiMinor7.Application.Common.Exceptions;
using SiMinor7.Application.Users.Models;
using SiMinor7.Domain.Entities;

namespace SiMinor7.Application.Users.Queries.GetUserDetail;

public record GetUserDetailQuery(string Id) : IRequest<UserDetailDto>;

public class GetUserDetailQueryHandler : IRequestHandler<GetUserDetailQuery, UserDetailDto>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IMapper _mapper;

    public GetUserDetailQueryHandler(UserManager<ApplicationUser> userManager, IMapper mapper)
    {
        _userManager = userManager;
        _mapper = mapper;
    }

    public async Task<UserDetailDto> Handle(GetUserDetailQuery request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.Id);

        if (user is null)
        {
            throw new NotFoundException();
        }

        return _mapper.Map<UserDetailDto>(user);
    }
}