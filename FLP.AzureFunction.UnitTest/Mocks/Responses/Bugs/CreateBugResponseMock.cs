using FLP.Application.Responses.Bugs;
using FLP.Core.Context.Constants;

namespace FLP.AzureFunction.UnitTest.Mocks.Responses.Bugs;

internal class CreateBugResponseMock : BasicMock<CreateBugReponse>
{
    public CreateBugResponseMock()
    {
        Faker.RuleFor(x => x.Title, f => f.Lorem.Sentence())
             .RuleFor(x => x.Description, f => f.Lorem.Sentences())
             .RuleFor(x => x.AssignedToUserId, f => f.Random.Guid())
             .RuleFor(x => x.Id, f => f.Random.Uuid())
             .RuleFor(x => x.Status, f => f.Random.Enum<Core.Context.Constants.BugStatus>())
             .CustomInstantiator(f => new CreateBugReponse(f.Random.Uuid(), f.Lorem.Sentence(3), f.Lorem.Sentences(), f.Random.Enum<BugStatus>() , f.Random.Guid()));
    }

    public CreateBugResponseMock WithTitle(string title)
    {
        Faker.RuleFor(x => x.Title, title);
        return this;
    }

    public CreateBugResponseMock WithDescription(string description)
    {
        Faker.RuleFor(x => x.Description, description);
        return this;
    }

    public CreateBugResponseMock WithAssignedToUserId(Guid? assignedToUserId)
    {
        Faker.RuleFor(x => x.AssignedToUserId, assignedToUserId);
        return this;
    }

    public CreateBugResponseMock WithId(Guid id)
    {
        Faker.RuleFor(x => x.Id, id);
        return this;
    }
    public CreateBugResponseMock WithStatus(BugStatus status)
    {
        Faker.RuleFor(x => x.Status, status);
        return this;
    }
}
