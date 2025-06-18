using FLP.Core.Context.Constants;
using FLP.Core.Context.Shared;

namespace FLP.Core.Context.Query;

public record PaginatedBugQuery : PaginatedQuery
{
    public BugStatus? Status { get; set; }
}
