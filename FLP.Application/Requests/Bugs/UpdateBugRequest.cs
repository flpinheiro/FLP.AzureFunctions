using FLP.Application.Responses.Bugs;
using FLP.Core.Context.Constants;
using FLP.Core.Context.Shared;
using MediatR;

namespace FLP.Application.Requests.Bugs;

public record UpdateBugRequest : IRequest<BaseResponse<GetBugByIdResponse>>
{
    public Guid Id { get; set; }

    public string? Title { get; set; }
    public string? Description { get; set; }
    public BugStatus Status { get; set; } // e.g., "Open", "In Progress", "Resolved"

    public Guid? AssignedToUserId { get; set; } // Nullable to allow for unassigned bugs
}
