using FLP.Core.Context.Constants;
using FLP.Core.Context.Main;
using FLP.Core.Context.Query;
using FLP.Core.Context.Shared;

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
    /// Gets all bugs in the repository, paginated according to the provided query parameters.
    /// </summary>
    /// <returns>A list of all bugs.</returns>
    Task<IEnumerable<Bug>> GetAsync(PaginatedBugQuery query,CancellationToken cancellationToken);

    /// <summary>
    /// count all bugs in the repository, paginated according to the provided query parameters.
    /// </summary>
    /// <param name="query"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<int> CountAsync(PaginatedBugQuery query, CancellationToken cancellationToken);
}
