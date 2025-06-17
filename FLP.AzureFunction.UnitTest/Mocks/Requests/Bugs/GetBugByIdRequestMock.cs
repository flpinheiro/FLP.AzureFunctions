using FLP.Application.Requests.Bugs;

namespace FLP.AzureFunction.UnitTest.Mocks.Requests.Bugs;

internal class GetBugByIdRequestMock : BasicMock<GetBugByIdRequest>
{
    public GetBugByIdRequestMock()
    {
        Faker.RuleFor(x => x.Id, f => f.Random.Guid())
            .CustomInstantiator(f => new GetBugByIdRequest(f.Random.Guid()));
    }
    public GetBugByIdRequestMock WithId(Guid id)
    {
        Faker.RuleFor(x => x.Id, id);
        return this;
    }
}
