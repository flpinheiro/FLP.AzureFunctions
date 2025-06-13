using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FLP.Application.Responses.Bugs;
using MediatR;

namespace FLP.Application.Requests.Bugs;

internal class BugRequest
{
}
public record CreateBugRequest: IRequest<CreateBugReponse>
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
