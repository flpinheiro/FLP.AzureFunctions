using FLP.Application.Requests.Bugs;
using FLP.Application.Validators.Bugs;
using FLP.Core.Context.Shared;
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
public class DeleteBugHandler(IUnitOfWork _uow, /*IMapper _mapper,*/ ILogger<DeleteBugHandler> _logger) : IRequestHandler<DeleteBugRequest, BaseResponse>
{
    /// <summary>
    /// Handles the deletion of a bug based on the provided DeleteBugRequest.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<BaseResponse> Handle(DeleteBugRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        _logger.LogInformation("Handling DeleteBugRequest for Bug ID: {BugId}", request.Id);

        var validator = new DeleteBugValidator();
        var validationResult = validator.Validate(request);

        if (!validationResult.IsValid)
        {
            _logger.LogWarning("Validation failed for DeleteBugRequest: {Errors}", validationResult.Errors);
            return new BaseResponse(validationResult.Errors.Select(e => e.ErrorMessage));
        }

        // Begin a transaction
        _uow.BeginTransaction(cancellationToken);

        // Delete the bug
        await _uow.BugRepository.DeleteAsync(request.Id, cancellationToken);

        // Save changes
        await _uow.SaveChangesAsync(cancellationToken);

        // Commit the transaction
        _uow.CommitTransaction(cancellationToken);

        return new BaseResponse();
    }
}
