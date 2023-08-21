using SiMinor7.Application.Common.Constants;

namespace SiMinor7.Application.Common.Models.Paging;

public class PageQueryBase
{
    public int PageNumber { get; init; } = App.StartPageNumber;
    public int PageSize { get; init; } = App.PageSize;
    public IEnumerable<SortCriteria>? SortCriteria { get; set; }
    public string? SearchText { get; init; }
}