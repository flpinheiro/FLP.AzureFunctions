using FLP.Core.Context.Constants;

namespace FLP.Application.Responses.Bugs;

public record GetBugByIdResponse
{
    public Guid Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public BugStatus Status { get; set; } // e.g., "Open", "In Progress", "Resolved"
    public DateTime? ResolvedAt { get; set; } // Nullable to allow for unresolved bugs
    public Guid? AssignedToUserId { get; set; } // Nullable to allow for unassigned bugs
}
