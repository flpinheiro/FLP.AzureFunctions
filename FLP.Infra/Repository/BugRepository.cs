using FLP.Core.Context.Main;
using FLP.Core.Context.Query;
using FLP.Core.Exceptions;
using FLP.Core.Interfaces.Repository;
using FLP.Infra.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FLP.Infra.Repository;

internal class BugRepository(AppDbContext _context, ILogger<IBugRepository> _logger) : IBugRepository
{
    public async Task<Bug> AddAsync(Bug bug, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        _logger.LogInformation("Adding a new bug with Title: {Title}", bug.Title);
        await _context.Bugs.AddAsync(bug, cancellationToken);
        _logger.LogInformation("Bug with ID: {Id} added successfully.", bug.Id);
        return bug;
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        _logger.LogInformation("Deleting bug with ID: {Id}", id);
        var bug = await _context.Bugs.FindAsync(id, cancellationToken);
        if (bug != null)
        {
            _context.Bugs.Remove(bug);
            _logger.LogInformation("Bug with ID: {Id} deleted successfully.", id);
        }
        else
        {
            _logger.LogWarning("Bug with ID: {Id} not found for deletion.", id);
            throw new NotFoundException($"Bug with ID {id} not found.");
        }
    }

    private IQueryable<Bug> FilterBugsQuery(PaginatedBugQuery query)
    {
        var queryable = _context.Bugs
            .OrderByDescending(x => x.CreatedAt)
            .AsNoTracking()
            .AsQueryable();
        if (!string.IsNullOrEmpty(query.Query))
        {
            queryable = queryable.Where(x =>
            (x.Title != null && x.Title.Contains(query.Query)) || (x.Description != null && x.Description.Contains(query.Query)));
        }
        if (query.Status.HasValue)
        {
            queryable = queryable.Where(x => x.Status == query.Status);
        }
        return queryable;
    }
    public async Task<IEnumerable<Bug>> GetAsync(PaginatedBugQuery query, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        _logger.LogInformation("Retrieving all bugs from the repository.");
        return await FilterBugsQuery(query)
            .Skip((query.Page - 1) * query.PageSize)
            .Take(query.PageSize)
            .ToListAsync(cancellationToken);
    }

    public async Task<int> CountAsync(PaginatedBugQuery query, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        _logger.LogInformation("Counting all bugs in the repository.");
        return await FilterBugsQuery(query).CountAsync(cancellationToken);
    }

    public async Task<Bug?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        _logger.LogInformation("Retrieving bug with ID: {Id}", id);
        return await _context.Bugs.FindAsync(id, cancellationToken);
    }

    public async Task<Bug> UpdateAsync(Bug bug, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        _logger.LogInformation("Updating bug with ID: {Id}", bug.Id);
        await Task.FromResult(_context.Bugs.Update(bug));
        _logger.LogInformation("Bug with ID: {Id} updated successfully.", bug.Id);
        return bug;
    }
}
