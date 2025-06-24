using FLP.Application.Responses.Bugs;
using FLP.Core.Context.Query;
using FLP.Core.Context.Shared;
using MediatR;

namespace FLP.Application.Requests.Bugs;

public record GetBugsPaginatedRequest : PaginatedBugQuery, IRequest<BaseResponse<GetBugsPaginatedResponse>>;
