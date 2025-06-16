using AutoMapper;
using FLP.Application.Requests.Bugs;
using FLP.Application.Responses.Bugs;
using FLP.Application.Validators.Bugs;
using FLP.Core.Context.Main;
using FLP.Core.Interfaces.Repository;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FLP.Application.Handlers.Bugs;

public class CreateBugHandler(IUnitOfWork _uow, IMapper _mapper, ILogger<CreateBugHandler> _logger) : IRequestHandler<CreateBugRequest, CreateBugReponse>
{
    public async Task<CreateBugReponse> Handle(CreateBugRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        _logger.LogInformation("Handling CreateBugRequest for Title: {Title}", request.Title);
        // Validate the request
        var validator = new CreateBugValidator();
        var validationResult = validator.Validate(request);
        if (!validationResult.IsValid)
        {
            // Handle validation errors
            throw new ArgumentException(string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage)));
        }

        try
        {
            // Map the request to a domain entity
            var bug = _mapper.Map<Bug>(request);
            _uow.BeginTransaction(cancellationToken);
            // Add the bug to the repository
            var addedBug = await _uow.BugRepository.AddAsync(bug, cancellationToken);
            await _uow.SaveChangesAsync(cancellationToken);

            // Map the added bug to a response DTO
            var response = _mapper.Map<CreateBugReponse>(addedBug);

            // Commit the transaction
            _uow.CommitTransaction(cancellationToken);

            _logger.LogInformation("Bug created successfully with ID: {Id}", response.Id);

            return response;
        }
        catch (Exception ex)
        {
            // Rollback the transaction in case of an error
            _uow.RollbackTransaction();
            _logger.LogError(ex, "An error occurred while creating the bug with Title: {Title}", request.Title);
            throw new Exception("An error occurred while creating the bug.", ex);
        }
    }
}
