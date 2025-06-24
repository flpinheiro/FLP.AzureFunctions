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

public class GetBugByIdHandler(IUnitOfWork _uow, IMapper _mapper, ILogger<GetBugByIdHandler> _logger) : IRequestHandler<GetBugByIdRequest, BaseResponse<GetBugByIdResponse>>
{
    /// <summary>
    /// runs the GetBugByIdRequest handler to retrieve a bug by its ID.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="NotFoundException"></exception>
    public async Task<BaseResponse<GetBugByIdResponse>> Handle(GetBugByIdRequest request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        _logger.LogInformation("Handling GetBugByIdRequest for Id: {Id}", request.Id);

        // Validate the request
        var validator = new GetBugByIdValidator();
        var validationResult = validator.Validate(request);
        if (!validationResult.IsValid)
        {
            _logger.LogWarning("Validation failed for GetBugByIdRequest: {Errors}", validationResult.Errors);
            return new BaseResponse<GetBugByIdResponse>(false, validationResult.Errors.Select(e => e.ErrorMessage));
        }

        var bug = await _uow.BugRepository.GetByIdAsync(request.Id, cancellationToken);

        if (bug == null)
        {
            return new BaseResponse<GetBugByIdResponse>(false, "Bug not found");
        }

        _logger.LogInformation("Bug found with Id: {Id}", request.Id);
        // Map the bug entity to a response DTO
        var response = _mapper.Map<GetBugByIdResponse>(bug);
        return new BaseResponse<GetBugByIdResponse>(response);
    }
}
