namespace FLP.Core.Interfaces.Repository;

public interface IUnitOfWork : IDisposable
{
    /// <summary>
    /// Represents the repository for managing bugs within this unit of work.
    /// </summary>
    public IBugRepository BugRepository { get; }
    /// <summary>
    /// Saves all changes made in this unit of work to the database.
    /// </summary>
    /// <returns>The number of state entries written to the database.</returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    /// <summary>
    /// Begins a new transaction for this unit of work.
    /// </summary>
    void BeginTransaction(CancellationToken cancellationToken);
    /// <summary>
    /// Commits the current transaction for this unit of work.
    /// </summary>
    void CommitTransaction(CancellationToken cancellationToken);
    /// <summary>
    /// Rolls back the current transaction for this unit of work.
    /// </summary>
    void RollbackTransaction();
}
