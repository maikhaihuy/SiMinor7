﻿using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SiMinor7.Application.Common.Extensions;
using SiMinor7.Application.Common.Interfaces;
using SiMinor7.Application.Common.Models.Paging;
using SiMinor7.Application.Users.Models;
using SiMinor7.Domain.Entities;

namespace SiMinor7.Application.Users.Queries.GetUsersWithPagination;

public class GetUsersWithPaginationQuery : PageQueryBase, IRequest<PaginatedList<UserListDto>>
{
    public string Role { get; set; } = string.Empty;
}

public class GetUsersWithPaginationQueryHandler : IRequestHandler<GetUsersWithPaginationQuery, PaginatedList<UserListDto>>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetUsersWithPaginationQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<PaginatedList<UserListDto>> Handle(GetUsersWithPaginationQuery request, CancellationToken cancellationToken)
    {
        IQueryable<ApplicationUser> query = _dbContext.Users;

        if (!string.IsNullOrEmpty(request.Role))
        {
            query = query.Include(u => u.Roles.Where(r => r.Name == request.Role));
        }

        if (!string.IsNullOrEmpty(request.SearchText))
        {
            query = query.Where(x =>
                (!string.IsNullOrEmpty(x.FirstName) && x.FirstName.Contains(request.SearchText))
                || (!string.IsNullOrEmpty(x.LastName) && x.LastName.Contains(request.SearchText))
                || (!string.IsNullOrEmpty(x.Email) && x.Email.Contains(request.SearchText))
                || x.Roles.Any(y => y.Name != null && y.Name.Contains(request.SearchText)));
        }

        if (request.SortCriteria.IsNullOrEmpty())
        {
            query = query.OrderBy(x => x.FirstName);
        }

        var result = await query.PaginatedListAsync<UserListDto>(request.PageNumber, request.PageSize, _mapper.ConfigurationProvider, cancellationToken);

        return result;
    }
}