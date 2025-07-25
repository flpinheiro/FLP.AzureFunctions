﻿using FLP.Application.Responses.Bugs;
using FLP.Core.Context.Shared;
using MediatR;

namespace FLP.Application.Requests.Bugs;

public record CreateBugRequest : IRequest<BaseResponse<GetBugByIdResponse>>
{
    public string Title { get; set; }
    public string Description { get; set; }
    public Guid? AssignedToUserId { get; set; } // Nullable to allow for unassigned bugs
    public CreateBugRequest(string title, string description, Guid? assignedToUserId = null)
    {
        Title = title;
        Description = description;
        AssignedToUserId = assignedToUserId;
    }
}
