using FLP.Core.Context.Constants;

namespace FLP.Application.Responses.Bugs;

public record UpdateBugResponse
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public Guid? AssignedToUserId { get; set; }
    public BugStatus Status { get; set; }

    public DateTime? ResolvedAt { get; set; }
}
