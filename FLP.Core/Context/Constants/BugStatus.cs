using System.ComponentModel;

namespace FLP.Core.Context.Constants;

public enum BugStatus
{
    [Description("Open")]
    Open = 0,
    [Description("In Progress")]
    InProgress = 1,
    [Description("Resolved")]
    Resolved = 2,
    [Description("Closed")]
    Closed = 3,
}
