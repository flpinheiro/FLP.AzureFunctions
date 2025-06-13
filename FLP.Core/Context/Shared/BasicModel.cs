using FLP.Core.Interfaces.Shared;

namespace FLP.Core.Context.Shared;

public abstract class BasicModel<T> : IAuditable
    where T : IEquatable<T>
{
    public T? Id { get; set; }
    public virtual DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
