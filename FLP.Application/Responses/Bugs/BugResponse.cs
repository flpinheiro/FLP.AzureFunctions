using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    public CreateBugReponse(Guid id, string title, string description, Guid? assignedToUserId = null)
    {
        Id = id;
        Title = title;
        Description = description;
        AssignedToUserId = assignedToUserId;
    }
}
