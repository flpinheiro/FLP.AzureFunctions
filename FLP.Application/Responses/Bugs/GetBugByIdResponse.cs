using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FLP.Core.Context.Constants;

namespace FLP.Application.Responses.Bugs;

public record GetBugByIdResponse
{
    public Guid Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public BugStatus Status { get; private set; } // e.g., "Open", "In Progress", "Resolved"
    public DateTime? ResolvedAt { get; private set; } // Nullable to allow for unresolved bugs
    public Guid? AssignedToUserId { get; set; } // Nullable to allow for unassigned bugs
}
