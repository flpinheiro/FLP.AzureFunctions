using AutoMapper;
using FLP.Application.Requests.Bugs;
using FLP.Application.Responses.Bugs;
using FLP.Application.Validators.Bugs;
using FLP.Core.Context.Main;
using FLP.Core.Context.Shared;
using FLP.Core.Interfaces.Repository;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FLP.Application.Handlers.Bugs;

public class CreateBugHandler(IUnitOfWork _uow, IMapper _mapper, ILogger<CreateBugHandler> _logger) : IRequestHandler<CreateBugRequest, BaseResponse<GetBugByIdResponse>>
{
    public async Task<BaseResponse<GetBugByIdResponse>> Handle(CreateBugRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        _logger.LogInformation("Handling CreateBugRequest for Title: {Title}", request.Title);
        // Validate the request
        var validator = new CreateBugValidator();
        var validationResult = validator.Validate(request);
        if (!validationResult.IsValid)
        {
            _logger.LogWarning("Validation failed for CreateBugRequest: {Errors}", validationResult.Errors);
            return new BaseResponse<GetBugByIdResponse>(validationResult.Errors.Select(e => e.ErrorMessage));
        }

        // Map the request to a domain entity
        var bug = _mapper.Map<Bug>(request);
        _uow.BeginTransaction(cancellationToken);
        // Add the bug to the repository
        var addedBug = await _uow.BugRepository.AddAsync(bug, cancellationToken);
        await _uow.SaveChangesAsync(cancellationToken);

        // Map the added bug to a response DTO
        var response = _mapper.Map<GetBugByIdResponse>(addedBug);

        // Commit the transaction
        _uow.CommitTransaction(cancellationToken);

        _logger.LogInformation("Bug created successfully with ID: {Id}", response.Id);

        return new BaseResponse<GetBugByIdResponse>(response);
    }
}
