using FLP.Application.Requests.Bugs;

namespace FLP.AzureFunction.UnitTest.Fixtures.Requests.Bugs;

internal class CreateBugRequestFixture : BasicFixture<CreateBugRequest>
{
    public CreateBugRequestFixture()
    {
        Faker.RuleFor(x => x.Title, f => f.Lorem.Sentence(3))
            .RuleFor(x => x.Description, f => f.Lorem.Sentences())
            .RuleFor(x => x.AssignedToUserId, f => f.Random.Guid())
            .CustomInstantiator(f => new CreateBugRequest(f.Lorem.Sentence(3), f.Lorem.Sentences(), f.Random.Guid()));
    }

    public CreateBugRequestFixture WithTitle(string? title)
    {
        Faker.RuleFor(x => x.Title, title);
        return this;
    }

    public CreateBugRequestFixture WithDescription(string? description)
    {
        Faker.RuleFor(x => x.Description, description);
        return this;
    }

    public CreateBugRequestFixture WithAssignedToUserId(Guid? assignedToUserId)
    {
        Faker.RuleFor(x => x.AssignedToUserId, assignedToUserId);
        return this;
    }
}
