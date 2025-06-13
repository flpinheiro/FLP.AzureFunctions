using FLP.Core.Context.Constants;
using FLP.Core.Context.Main;
using FLP.Core.Interfaces.Repository;
using FLP.Infra.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FLP.Infra.Repository;

internal class BugRepository(AppDbContext _context, ILogger<IBugRepository> _logger) : IBugRepository
{
    public async Task<Bug> AddAsync(Bug bug)
    {
        _logger.LogInformation("Adding a new bug with Title: {Title}", bug.Title);
        await _context.Bugs.AddAsync(bug);
        _logger.LogInformation("Bug with ID: {Id} added successfully.", bug.Id);
        return bug;
    }

    public async Task DeleteAsync(Guid id)
    {
        _logger.LogInformation("Deleting bug with ID: {Id}", id);
        var bug = await _context.Bugs.FindAsync(id);
        if (bug != null)
        {
            _context.Bugs.Remove(bug);
            _logger.LogInformation("Bug with ID: {Id} deleted successfully.", id);
        }
        else
        {
            _logger.LogWarning("Bug with ID: {Id} not found for deletion.", id);
            throw new KeyNotFoundException($"Bug with ID {id} not found.");
        }
    }

    public async Task<IEnumerable<Bug>> GetAllAsync()
    {
        _logger.LogInformation("Retrieving all bugs from the repository.");
        return await _context.Bugs.ToListAsync();
        
    }

    public async Task<Bug?> GetByIdAsync(Guid id)
    {
        _logger.LogInformation("Retrieving bug with ID: {Id}", id);
        return await _context.Bugs.FindAsync(id);
    }

    public async Task<IEnumerable<Bug>> GetByStatusAsync(BugStatus status)
    {
        _logger.LogInformation("Retrieving bugs with status: {Status}", status);
        return await _context.Bugs
            .Where(b => b.Status == status)
            .ToListAsync();
    }

    public async Task<Bug> UpdateAsync(Bug bug)
    {
        _logger.LogInformation("Updating bug with ID: {Id}", bug.Id);
        await Task.FromResult( _context.Bugs.Update(bug));
        _logger.LogInformation("Bug with ID: {Id} updated successfully.", bug.Id);
        return bug;
    }
}
