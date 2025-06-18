using MediatR;

namespace FLP.Application.Requests.Bugs;

public record DeleteBugRequest(Guid Id): IRequest;