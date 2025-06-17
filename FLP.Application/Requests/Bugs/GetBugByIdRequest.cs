using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FLP.Application.Responses.Bugs;
using MediatR;

namespace FLP.Application.Requests.Bugs;

public record GetBugByIdRequest(Guid Id) :IRequest<GetBugByIdResponse>;
//{
//    public Guid Id { get; set; }
//    public GetBugByIdRequest(Guid id)
//    {
//        Id = id;
//    }
//}
