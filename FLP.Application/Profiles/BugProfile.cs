using AutoMapper;
using FLP.Application.Requests.Bugs;
using FLP.Application.Responses.Bugs;
using FLP.Core.Context.Main;

namespace FLP.Application.Profiles;

public class BugProfile : Profile
{
    public BugProfile()
    {
        CreateMap<CreateBugRequest, Bug>();
        CreateMap<Bug, GetBugByIdResponse>();
        CreateMap<Bug, GetBugResponse>();
    }
}
