using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FLP.Core.Context.Constants;
using FLP.Core.Context.Shared;

namespace FLP.Core.Context.Query;

public record PaginatedBugQuery : PaginatedQuery
{
    public BugStatus? Status { get; set; }
}
