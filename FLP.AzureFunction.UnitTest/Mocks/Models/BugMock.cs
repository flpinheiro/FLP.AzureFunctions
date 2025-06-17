using FLP.Core.Context.Constants;
using FLP.Core.Context.Main;

namespace FLP.AzureFunction.UnitTest.Mocks.Models;

internal class BugMock : BasicMock<Bug>
{
    public BugMock()
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
    public BugMock WithId(Guid id)
    {
        Faker.RuleFor(x => x.Id, id);
        return this;
    }

    public BugMock WithTitle(string title)
    {
        Faker.RuleFor(x => x.Title, title);
        return this;
    }

    public BugMock WithDescription(string description)
    {
        Faker.RuleFor(x => x.Description, description);
        return this;
    }

    public BugMock WithAssignedToUserId(Guid? assignedToUserId)
    {
        Faker.RuleFor(x => x.AssignedToUserId, assignedToUserId);
        return this;
    }

    public BugMock WithStatus(BugStatus status)
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
