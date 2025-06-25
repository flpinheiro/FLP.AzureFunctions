using FLP.Core.Context.Constants;

namespace FLP.Core.Context.Shared;

public record PaginatedQuery
{
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string? Query { get; set; }
    public string? SortBy { get; set; }
    public SortOrder SortOrder { get; set;} = SortOrder.Ascending;
}
