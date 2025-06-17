using FLP.Application.Requests.Bugs;

namespace FLP.AzureFunction.UnitTest.Mocks.Requests.Bugs;

internal class CreateBugRequestMock : BasicMock<CreateBugRequest>
{
    public CreateBugRequestMock()
    {
        Faker.RuleFor(x => x.Title, f => f.Lorem.Sentence(3))
            .RuleFor(x => x.Description, f => f.Lorem.Sentences())
            .RuleFor(x => x.AssignedToUserId, f => f.Random.Guid())
            .CustomInstantiator(f => new CreateBugRequest(f.Lorem.Sentence(3), f.Lorem.Sentences(), f.Random.Guid()));
    }

    public CreateBugRequestMock WithTitle(string? title)
    {
        Faker.RuleFor(x => x.Title, title);
        return this;
    }

    public CreateBugRequestMock WithDescription(string? description)
    {
        Faker.RuleFor(x => x.Description, description);
        return this;
    }

    public CreateBugRequestMock WithAssignedToUserId(Guid? assignedToUserId)
    {
        Faker.RuleFor(x => x.AssignedToUserId, assignedToUserId);
        return this;
    }
}
