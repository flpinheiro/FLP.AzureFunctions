using FLP.Application.Responses.Bugs;
using FLP.Core.Context.Constants;

namespace FLP.AzureFunction.UnitTest.Fixtures.Responses.Bugs;

internal class CreateBugResponseFixture : BasicFixture<CreateBugReponse>
{
    public CreateBugResponseFixture()
    {
        Faker.RuleFor(x => x.Title, f => f.Lorem.Sentence())
             .RuleFor(x => x.Description, f => f.Lorem.Sentences())
             .RuleFor(x => x.AssignedToUserId, f => f.Random.Guid())
             .RuleFor(x => x.Id, f => f.Random.Uuid())
             .RuleFor(x => x.Status, f => f.Random.Enum<BugStatus>())
             .CustomInstantiator(f => new CreateBugReponse(f.Random.Uuid(), f.Lorem.Sentence(3), f.Lorem.Sentences(), f.Random.Enum<BugStatus>(), f.Random.Guid()));
    }

    public CreateBugResponseFixture WithTitle(string title)
    {
        Faker.RuleFor(x => x.Title, title);
        return this;
    }

    public CreateBugResponseFixture WithDescription(string description)
    {
        Faker.RuleFor(x => x.Description, description);
        return this;
    }

    public CreateBugResponseFixture WithAssignedToUserId(Guid? assignedToUserId)
    {
        Faker.RuleFor(x => x.AssignedToUserId, assignedToUserId);
        return this;
    }

    public CreateBugResponseFixture WithId(Guid id)
    {
        Faker.RuleFor(x => x.Id, id);
        return this;
    }
    public CreateBugResponseFixture WithStatus(BugStatus status)
    {
        Faker.RuleFor(x => x.Status, status);
        return this;
    }
}
