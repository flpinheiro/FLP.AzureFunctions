using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FLP.Core.Context.Constants;

namespace FLP.Application.Responses.Bugs;

internal class BugResponse
{
}

public record CreateBugReponse
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public Guid? AssignedToUserId { get; set; } // Nullable to allow for unassigned bugs
    public BugStatus Status { get; set; }
    public CreateBugReponse(Guid id, string title, string description, BugStatus status, Guid? assignedToUserId = null)
    {
        Id = id;
        Title = title;
        Description = description;
        Status = status;
        AssignedToUserId = assignedToUserId;
    }
}
