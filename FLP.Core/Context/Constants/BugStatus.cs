using System.ComponentModel;

namespace FLP.Core.Context.Constants;

public enum BugStatus
{
    [Description("Open")]
    Open,
    [Description("In Progress")]
    InProgress,
    [Description("Resolved")]
    Resolved,
    [Description("Closed")]
    Closed,
}
