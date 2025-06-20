using FLP.Application.Requests.Bugs;

namespace FLP.AzureFunction.UnitTest.Fixtures.Requests.Bugs;

internal class UpdateBugRequestFixture : BasicFixture<UpdateBugRequest>
{
    public UpdateBugRequestFixture()
    {
        Faker
            .RuleFor(x => x.Id, f => f.Random.Guid())
            .RuleFor(x => x.Title, f => f.Lorem.Sentence(3))
            .RuleFor(x => x.Description, f => f.Lorem.Sentences())
            .RuleFor(x => x.Status, f => f.PickRandom<Core.Context.Constants.BugStatus>())
            .RuleFor(x => x.AssignedToUserId, f => f.Random.Guid());
    }

    public UpdateBugRequestFixture WithId(Guid id)
    {
        Faker.RuleFor(x => x.Id, id);
        return this;
    }

    public UpdateBugRequestFixture WithTitle(string? title)
    {
        Faker.RuleFor(x => x.Title, title);
        return this;
    }

    public UpdateBugRequestFixture WithDescription(string? description)
    {
        Faker.RuleFor(x => x.Description, description);
        return this;
    }

    public UpdateBugRequestFixture WithAssignedToUserId(Guid? assignedToUserId)
    {
        Faker.RuleFor(x => x.AssignedToUserId, assignedToUserId);
        return this;
    }

    public UpdateBugRequestFixture WithStatus(Core.Context.Constants.BugStatus status)
    {
        Faker.RuleFor(x => x.Status, status);
        return this;
    }
}
