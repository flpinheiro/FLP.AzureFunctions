using FLP.Core.Context.Constants;
using FLP.Core.Context.Shared;

namespace FLP.Core.Context.Main;

public class Bug : BasicModel<Guid>
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
        if (IsBugResolvedOrClosed(newStatus))
        {
            ResolvedAt = DateTime.UtcNow; // Set resolved time when status is changed to Resolved
        }
        else
        {
            ResolvedAt = null; // Clear resolved time for statuses other than Resolved
        }
    }

    /// <summary>
    /// Checks if the bug status is either Resolved or Closed.
    /// </summary>
    /// <param name="status"></param>
    /// <returns></returns>
    public static bool IsBugResolvedOrClosed(BugStatus status)
        => status == BugStatus.Resolved || status == BugStatus.Closed;

    /// <summary>
    /// Checks if the current bug status is either Resolved or Closed.
    /// </summary>
    /// <returns></returns>
    public bool IsBugResolvedOrClosed()
    {
        return IsBugResolvedOrClosed(Status);
    }
}
