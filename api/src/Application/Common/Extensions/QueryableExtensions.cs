
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SiMinor7.Application.Common.Models.Paging;

namespace SiMinor7.Application.Common.Extensions;

public static class QueryableExtensions
{
    public static Task<PaginatedList<TDestination>> PaginatedListAsync<TDestination>(this IQueryable queryable, int pageNumber, int pageSize, AutoMapper.IConfigurationProvider configuration, CancellationToken cancellation) where TDestination : class
        => PaginatedList<TDestination>.CreateAsync(queryable.ProjectTo<TDestination>(configuration).AsNoTracking(), pageNumber, pageSize, cancellation);

    public static Task<List<TDestination>> ProjectToListAsync<TDestination>(this IQueryable queryable, AutoMapper.IConfigurationProvider configuration, CancellationToken cancellation) where TDestination : class
        => queryable.ProjectTo<TDestination>(configuration).AsNoTracking().ToListAsync(cancellation);
}