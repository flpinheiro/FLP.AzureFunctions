using FLP.Core.Context.Constants;

namespace FLP.Application.Responses.Bugs;

public record GetBugResponse
{
    public Guid Id { get; set; }
    public string? Title { get; set; }
    public BugStatus Status { get; set; }
}

public record GetBugsResponse(IEnumerable<GetBugResponse> Bugs, int Total);
