using FLP.Application.Requests.Bugs;
using FLP.Application.Validators.Bugs;
using FLP.Core.Exceptions;
using FLP.Core.Interfaces.Repository;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FLP.Application.Handlers.Bugs;

/// <summary>
/// Handler for processing DeleteBugRequest to delete a bug from the repository.
/// </summary>
/// <param name="_uow"></param>
/// <param name="_mapper"></param>
/// <param name="_logger"></param>
public class DeleteBugHandler(IUnitOfWork _uow, /*IMapper _mapper,*/ ILogger<DeleteBugHandler> _logger) : IRequestHandler<DeleteBugRequest>
{
    /// <summary>
    /// Handles the deletion of a bug based on the provided DeleteBugRequest.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task Handle(DeleteBugRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        _logger.LogInformation("Handling DeleteBugRequest for Bug ID: {BugId}", request.Id);

        var validator = new DeleteBugValidator();
        var validationResult = validator.Validate(request);

        if (!validationResult.IsValid)
        {
            throw new ArgumentException("Validation failed: " + string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage)), nameof(request));
        }

        try
        {
            // Begin a transaction
            _uow.BeginTransaction(cancellationToken);

            // Delete the bug
            await _uow.BugRepository.DeleteAsync(request.Id, cancellationToken);

            // Save changes
            await _uow.SaveChangesAsync(cancellationToken);

            // Commit the transaction
            _uow.CommitTransaction(cancellationToken);

        }
        catch (NotFoundException)
        {
            _logger.LogWarning("Bug with ID: {BugId} not found for deletion.", request.Id);
            // Rollback the transaction if the bug was not found
            _uow.RollbackTransaction();
            throw;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while deleting the bug with ID: {BugId}", request.Id);
            // Rollback the transaction in case of any other error
            _uow.RollbackTransaction();
            throw;
        }
    }
}
