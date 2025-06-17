using FLP.Core.Interfaces.Repository;
using FLP.Infra.Data;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;

namespace FLP.Infra;

internal class UnitOfWork(AppDbContext _context, ILogger<UnitOfWork> _logger, Lazy<IBugRepository> _bugRepository) : IUnitOfWork
{
    private bool disposedValue = false;
    private IDbContextTransaction? _transaction;
    public IBugRepository BugRepository => _bugRepository.Value;


    #region transaction
    public void BeginTransaction(CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        _logger.LogInformation("Beginning a new transaction.");
        _transaction = _context.Database.BeginTransaction();
        _logger.LogInformation($"Transaction started successfully with Tansaction Id: {_transaction.TransactionId}");
    }

    public void CommitTransaction(CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        _logger.LogInformation($"Committing the current transaction with Tansaction Id: {_transaction?.TransactionId}");
        _context.Database.CommitTransaction();
        _logger.LogInformation($"Transaction committed successfully with Tansaction Id: {_transaction?.TransactionId}");
    }

    public void RollbackTransaction()
    {
        _logger.LogWarning($"Rolling back the current transaction with Tansaction Id: {_transaction?.TransactionId}");
        _context.Database.RollbackTransaction();
        _logger.LogWarning($"Transaction rolled back successfully with Tansaction Id: {_transaction?.TransactionId}");
    }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        _logger.LogInformation("Saving changes to the database.");
        // This will save all changes made in the context to the database
        // and return the number of state entries written to the database.
        return _context.SaveChangesAsync();
    }
    #endregion

    #region Dispose
    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                _logger.LogInformation("Disposing UnitOfWork resources.");
                // TODO: dispose managed state (managed objects)
                _transaction?.Dispose();
                _context.Dispose();
                _logger.LogInformation("UnitOfWork resources disposed successfully.");
            }

            // TODO: free unmanaged resources (unmanaged objects) and override finalizer
            // TODO: set large fields to null
            disposedValue = true;
        }
    }

    // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
    // ~UnitOfWork()
    // {
    //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
    //     Dispose(disposing: false);
    // }

    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
    #endregion
}
