using FLP.Application.Responses.Bugs;
using MediatR;

namespace FLP.Application.Requests.Bugs;

public record GetBugByIdRequest(Guid Id) : IRequest<GetBugByIdResponse>;
//{
//    public Guid Id { get; set; }
//    public GetBugByIdRequest(Guid id)
//    {
//        Id = id;
//    }
//}
