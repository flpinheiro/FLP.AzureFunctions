using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FLP.Core.Context.Constants;
using FLP.Core.Context.Shared;

namespace FLP.Core.Context.Main;

public class Bug: BasicModel<Guid>
{
    public Bug()
    {
        Status = BugStatus.Open; // Default status for a new bug
    }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public BugStatus Status { get; private set; } // e.g., "Open", "In Progress", "Resolved"
    public DateTime? ResolvedAt { get; private set; } // Nullable to allow for unresolved bugs
    public Guid? AssignedToUserId { get; set; } // Nullable to allow for unassigned bugs

    /// <summary>
    /// Updates the status of the bug and sets ResolvedAt if the status is changed to Resolved.
    /// </summary>
    /// <param name="newStatus"></param>
    public void UpdateStatus(BugStatus newStatus)
    {
        Status = newStatus;
        // Automatically set ResolvedAt when status changes to Resolved
        if (newStatus == BugStatus.Resolved)
        {
            ResolvedAt = DateTime.UtcNow; // Set resolved time when status is changed to Resolved
        }
        else
        {
            ResolvedAt = null; // Clear resolved time for statuses other than Resolved
        }
    }
}
