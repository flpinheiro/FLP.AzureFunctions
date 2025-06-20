using FLP.Application.Responses.Bugs;
using FLP.Core.Context.Shared;
using MediatR;

namespace FLP.Application.Requests.Bugs;

public record GetBugByIdRequest(Guid Id) : IRequest<BaseResponse<GetBugByIdResponse>>;

