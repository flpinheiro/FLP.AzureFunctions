using FLP.Core.Context.Constants;
using FLP.Core.Context.Main;

namespace FLP.Core.Interfaces.Repository;

public interface IBugRepository
{
    /// <summary>
    /// Adds a new bug to the repository.
    /// </summary>
    /// <param name="bug">The bug to add.</param>
    /// <returns>The added bug.</returns>
    Task<Bug> AddAsync(Bug bug, CancellationToken cancellationToken);
    /// <summary>
    /// Updates an existing bug in the repository.
    /// </summary>
    /// <param name="bug">The bug to update.</param>
    /// <returns>The updated bug.</returns>
    Task<Bug> UpdateAsync(Bug bug, CancellationToken cancellationToken);
    /// <summary>
    /// Deletes a bug from the repository.
    /// </summary>
    /// <param name="id">The ID of the bug to delete.</param>
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    /// <summary>
    /// Gets a bug by its ID.
    /// </summary>
    /// <param name="id">The ID of the bug to retrieve.</param>
    /// <returns>The retrieved bug.</returns>
    Task<Bug?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    /// <summary>
    /// Gets all bugs in the repository.
    /// </summary>
    /// <returns>A list of all bugs.</returns>
    Task<IEnumerable<Bug>> GetAllAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Gets all bugs with the specified status.
    /// </summary>
    /// <param name="status"></param>
    /// <returns></returns>
    Task<IEnumerable<Bug>> GetByStatusAsync(BugStatus status, CancellationToken cancellationToken);
}
