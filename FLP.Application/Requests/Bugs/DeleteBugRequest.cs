using FLP.Core.Context.Shared;
using MediatR;

namespace FLP.Application.Requests.Bugs;

public record DeleteBugRequest(Guid Id) : IRequest<BaseResponse>;