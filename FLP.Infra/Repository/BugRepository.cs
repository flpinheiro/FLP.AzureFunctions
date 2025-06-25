using FLP.Core.Context.Constants;
using FLP.Core.Context.Main;
using FLP.Core.Context.Query;
using FLP.Core.Exceptions;
using FLP.Core.Extensions;
using FLP.Core.Interfaces.Repository;
using FLP.Infra.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

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

    public IQueryable<Bug> FilterBugsQuery(PaginatedBugQuery query)
    {
        return _context.Bugs
            .Quering(query)
            .AsNoTracking()
            .AsQueryable();
    }
    public async Task<IEnumerable<Bug>> GetAsync(PaginatedBugQuery query, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        _logger.LogInformation("Retrieving bugs from the repository with {query}", query);
        return await FilterBugsQuery(query)
            .Ordering(query)
            .Skip((query.Page - 1) * query.PageSize)
            .Take(query.PageSize)
            .ToListAsync(cancellationToken);
    }

    public async Task<int> CountAsync(PaginatedBugQuery query, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        _logger.LogInformation("Counting bugs in the repository with {query}", query);
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
internal static class BugRepositoryExtensions
{
    public static IOrderedQueryable<Bug> Ordering(this IQueryable<Bug> queryable, PaginatedBugQuery query)
    {
        return (query.SortBy?.ToLower()) switch
        {
            "title"
            => query.SortOrder == SortOrder.Ascending ?
            queryable.OrderBy(x => x.Title) :
            queryable.OrderByDescending(x => x.Title),

            "status"
            => query.SortOrder == SortOrder.Ascending ?
            queryable.OrderBy(x => x.Status) :
            queryable.OrderByDescending(x => x.Status),

            "resolvedat"
            => query.SortOrder == SortOrder.Ascending ?
            queryable.OrderBy(x => x.ResolvedAt) :
            queryable.OrderByDescending(x => x.ResolvedAt),

            _
            => query.SortOrder == SortOrder.Ascending ?
            queryable.OrderBy(x => x.CreatedAt) :
            queryable.OrderByDescending(x => x.CreatedAt),
        };
    }

    public static IQueryable<Bug> Quering(this IQueryable<Bug> queryable, PaginatedBugQuery query)
    {
        return queryable
            .Where(FilterByStatus(query.Status))
            .Where(FilterByQuery(query.Query));
    }

    private static Expression<Func<Bug, bool>> FilterByStatus(BugStatus? status)
    {
        return x =>
            !status.HasValue || x.Status == status;
    }

    private static Expression<Func<Bug, bool>> FilterByQuery(string? query)
    {
        return x =>
            string.IsNullOrEmpty(query) ||
            (x.Title != null && x.Title.Contains(query)) ||
            (x.Description != null && x.Description.Contains(query));
    }
}