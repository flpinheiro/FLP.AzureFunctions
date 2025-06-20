using AutoMapper;
using FLP.Application.Requests.Bugs;
using FLP.Application.Responses.Bugs;
using FLP.Application.Validators.Bugs;
using FLP.Core.Context.Shared;
using FLP.Core.Exceptions;
using FLP.Core.Interfaces.Repository;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FLP.Application.Handlers.Bugs;

public class UpdateBugHandler(IUnitOfWork _uow, IMapper _mapper, ILogger<UpdateBugHandler> _logger) : IRequestHandler<UpdateBugRequest, BaseResponse<GetBugByIdResponse>>
{
    public async Task<BaseResponse<GetBugByIdResponse>> Handle(UpdateBugRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        _logger.LogInformation("Handling UpdateBugRequest for Bug ID: {BugId}", request.Id);

        // Validate the request
        var validator = new UpdateBugValidator();
        var validationResult = validator.Validate(request);
        if (!validationResult.IsValid)
        {
            _logger.LogWarning("Validation failed for UpdateBugRequest: {Errors}", validationResult.Errors);
            return new BaseResponse<GetBugByIdResponse>(validationResult.Errors.Select(e => e.ErrorMessage));
        }

        _uow.BeginTransaction(cancellationToken);

        var bug = await _uow.BugRepository.GetByIdAsync(request.Id, cancellationToken);

        if (bug == null)
        {
            _logger.LogWarning("Bug with ID: {BugId} not found for update.", request.Id);
            return new BaseResponse<GetBugByIdResponse>($"Bug with ID {request.Id} not found.");
        }
        // Map the request to the bug entity
        bug.Title = request.Title ?? bug.Title;
        bug.Description = request.Description ?? bug.Description;
        bug.UpdateStatus(request.Status);
        bug.AssignedToUserId = request.AssignedToUserId ?? bug.AssignedToUserId;
        // Update the bug in the repository
        await _uow.BugRepository.UpdateAsync(bug, cancellationToken);

        // Save changes
        await _uow.SaveChangesAsync(cancellationToken);

        // Commit the transaction
        _uow.CommitTransaction(cancellationToken);
        _logger.LogInformation("Bug with ID: {BugId} updated successfully.", request.Id);

        var response = _mapper.Map<GetBugByIdResponse>(bug);

        return new BaseResponse<GetBugByIdResponse>(response);
    }
}
