using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FLP.Application.Requests.Bugs;
using FLP.Application.Responses.Bugs;
using FLP.Core.Context.Main;

namespace FLP.Application.Profiles;

internal class BugProfile : Profile
{
    public BugProfile()
    {
        CreateMap<CreateBugRequest, Bug>();
        CreateMap<Bug, CreateBugReponse>();
    }
}
