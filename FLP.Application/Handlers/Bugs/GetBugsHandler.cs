using AutoMapper;
using FLP.Application.Requests.Bugs;
using FLP.Application.Responses.Bugs;
using FLP.Application.Validators.Bugs;
using FLP.Core.Context.Shared;
using FLP.Core.Interfaces.Repository;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FLP.Application.Handlers.Bugs;

public class GetBugsHandler(IUnitOfWork _uow, IMapper _mapper, ILogger<GetBugsHandler> _logger) : IRequestHandler<GetBugsRequest, BaseResponse<GetBugsResponse>>
{
    public async Task<BaseResponse<GetBugsResponse>> Handle(GetBugsRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("GetBugsHandler called with request: {@Request}", request);

        var validator = new GetBugsValidator();
        var validationResult = validator.Validate(request);

        if (!validationResult.IsValid)
        {
            _logger.LogWarning("Validation failed for GetBugsRequest: {Errors}", validationResult.Errors);
            return new BaseResponse<GetBugsResponse>(validationResult.Errors.Select(e => e.ErrorMessage));
        }

        // Retrieve bugs from the repository
        var bugs = await _uow.BugRepository.GetAsync(request, cancellationToken);
        var count = _uow.BugRepository.CountAsync(request, cancellationToken);

        _logger.LogInformation("Retrieved {Count} bugs.", bugs.Count());
        // Map the bugs to response DTOs
        var response = _mapper.Map<IEnumerable<GetBugResponse>>(bugs);

        _logger.LogInformation("Mapped bugs to response DTOs: {@Response}", response);

        return new BaseResponse<GetBugsResponse>(new GetBugsResponse(response, await count));
    }
}
