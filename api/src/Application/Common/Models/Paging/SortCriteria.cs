namespace SiMinor7.Application.Common.Models.Paging;

public class SortCriteria
{
    public string Field { get; set; } = string.Empty;

    public bool IsDesc { get; set; }
}