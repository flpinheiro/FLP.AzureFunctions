using FLP.Application.Responses.Bugs;
using FLP.Core.Context.Constants;

namespace FLP.AzureFunction.UnitTest.Fixtures.Responses.Bugs;

internal class GetBugByIdResponseFixture : BasicFixture<GetBugByIdResponse>
{
    public GetBugByIdResponseFixture()
    {
        Faker
            .RuleFor(x => x.Id, f => f.Random.Guid())
            .RuleFor(x => x.Title, f => f.Lorem.Sentence(3))
            .RuleFor(x => x.Description, f => f.Lorem.Sentences())
            .RuleFor(x => x.Status, f => f.PickRandom<BugStatus>())
            .RuleFor(x => x.ResolvedAt, (f, c) => c.Status == BugStatus.Resolved ? f.Date.Recent() : null)
            .RuleFor(x => x.AssignedToUserId, f => f.Random.Guid());
    }

    public GetBugByIdResponseFixture WithId(Guid id)
    {
        Faker.RuleFor(x => x.Id, id);
        return this;
    }
    public GetBugByIdResponseFixture WithTitle(string? title)
    {
        Faker.RuleFor(x => x.Title, title);
        return this;
    }
    public GetBugByIdResponseFixture WithDescription(string? description)
    {
        Faker.RuleFor(x => x.Description, description);
        return this;
    }
    public GetBugByIdResponseFixture WithAssignedToUserId(Guid? assignedToUserId)
    {
        Faker.RuleFor(x => x.AssignedToUserId, assignedToUserId);
        return this;
    }
    public GetBugByIdResponseFixture WithStatus(BugStatus status)
    {
        Faker
            .RuleFor(x => x.Status, status)
            .RuleFor(x => x.ResolvedAt, (f, c) => c.Status == BugStatus.Resolved ? f.Date.Recent() : null);
        return this;
    }
    public GetBugByIdResponseFixture WithResolvedAt(DateTime? resolvedAt)
    {
        Faker.RuleFor(x => x.ResolvedAt, resolvedAt);
        return this;
    }
}
