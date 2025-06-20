using FLP.Core.Context.Constants;
using FLP.Core.Context.Main;

namespace FLP.AzureFunction.UnitTest.Fixtures.Models;

internal class BugFixture : BasicFixture<Bug>
{
    public BugFixture()
    {
        Faker
            .RuleFor(x => x.Id, f => f.Random.Guid())
            .RuleFor(x => x.Title, f => f.Lorem.Sentence())
            .RuleFor(x => x.Description, f => f.Lorem.Sentences())
            .RuleFor(x => x.AssignedToUserId, f => f.Random.Guid())
            .RuleFor(x => x.Status, f => f.Random.Enum<BugStatus>())
            .RuleFor(x => x.ResolvedAt, (f, c) => c.Status == BugStatus.Resolved ? f.Date.Recent() : null)
            .RuleFor(x => x.CreatedAt, f => f.Date.Recent());
    }
    public BugFixture WithId(Guid id)
    {
        Faker.RuleFor(x => x.Id, id);
        return this;
    }

    public BugFixture WithTitle(string title)
    {
        Faker.RuleFor(x => x.Title, title);
        return this;
    }

    public BugFixture WithDescription(string description)
    {
        Faker.RuleFor(x => x.Description, description);
        return this;
    }

    public BugFixture WithAssignedToUserId(Guid? assignedToUserId)
    {
        Faker.RuleFor(x => x.AssignedToUserId, assignedToUserId);
        return this;
    }

    public BugFixture WithStatus(BugStatus status)
    {
        Faker.RuleFor(x => x.Status, status);
        return this;
    }

    //public BugMock WithResolvedAt(DateTime? resolvedAt)
    //{
    //    Faker.RuleFor(x => x.ResolvedAt, resolvedAt);
    //    return this;
    //}
}
