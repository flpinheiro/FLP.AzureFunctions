using AutoMapper;
using FLP.Application.Requests.Bugs;
using FLP.Application.Responses.Bugs;
using FLP.Application.Validators.Bugs;
using FLP.Core.Interfaces.Repository;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FLP.Application.Handlers.Bugs;

public class GetBugsHandler(IUnitOfWork _uow, IMapper _mapper, ILogger<GetBugsHandler> _logger) : IRequestHandler<GetBugsRequest, GetBugsResponse>
{
    public async Task<GetBugsResponse> Handle(GetBugsRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("GetBugsHandler called with request: {@Request}", request);

        var validator = new GetBugsValidator();
        var validationResult = validator.Validate(request);

        if (!validationResult.IsValid)
        {
            throw new ArgumentException("Validation failed: " + string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage)), nameof(request));
        }

        // Retrieve bugs from the repository
        var bugs = await _uow.BugRepository.GetAsync(request, cancellationToken);
        var count = _uow.BugRepository.CountAsync(request, cancellationToken);

        _logger.LogInformation("Retrieved {Count} bugs.", bugs.Count());
        // Map the bugs to response DTOs
        var response = _mapper.Map<IEnumerable<GetBugResponse>>(bugs);

        return new GetBugsResponse(response, await count);
    }
}
