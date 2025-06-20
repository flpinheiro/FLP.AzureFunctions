using FLP.Application.Requests.Bugs;

namespace FLP.AzureFunction.UnitTest.Fixtures.Requests.Bugs;

internal class GetBugByIdRequestFixture : BasicFixture<GetBugByIdRequest>
{
    public GetBugByIdRequestFixture()
    {
        Faker.RuleFor(x => x.Id, f => f.Random.Guid())
            .CustomInstantiator(f => new GetBugByIdRequest(f.Random.Guid()));
    }
    public GetBugByIdRequestFixture WithId(Guid id)
    {
        Faker.RuleFor(x => x.Id, id);
        return this;
    }
}
