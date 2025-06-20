using AutoMapper;
using FLP.Application.Handlers.Bugs;
using FLP.Application.Profiles;
using FLP.Application.Responses.Bugs;
using FLP.AzureFunction.UnitTest.Fixtures.Models;
using FLP.AzureFunction.UnitTest.Fixtures.Requests.Bugs;
using FLP.AzureFunction.UnitTest.Mocks;
using FLP.AzureFunction.UnitTest.Stubs;
using FLP.Core.Context.Constants;
using FLP.Core.Context.Main;
using Microsoft.Extensions.Logging;
using Moq;

namespace FLP.AzureFunction.UnitTest.Tests.Handlres.Bugs;

public class UpdateBugHandlerTest
{
    private readonly UnitOfWorkMock _uow = new();
    private readonly Mock<ILogger<UpdateBugHandler>> _logger = new();
    private readonly IMapper _mapper;

    private readonly UpdateBugHandler _handler;

    public UpdateBugHandlerTest()
    {
        _mapper = IMapperStub.GetMapper(new BugProfile());
        _handler = new UpdateBugHandler(_uow.Object, _mapper, _logger.Object);
    }

    [Theory]
    [InlineData(BugStatus.Open)]
    [InlineData(BugStatus.InProgress)]
    [InlineData(BugStatus.Resolved)]
    [InlineData(BugStatus.Closed)]
    public async Task Run_UpdateBug_Async(BugStatus bugStatus)
    {
        //arrange
        var request = new UpdateBugRequestFixture()
            .WithStatus(bugStatus)
            .Generate();

        var bug = new BugFixture()
            .WithId(request.Id)
            .Generate();

        _uow
            .SetupBeginTransaction()
            .SetupSaveChangesAsync()
            .SetupCommitTransaction();

        _uow.BugRepository
            .SetupGetByIdAsync(bug)
            .SetupUpdateAsync(bug);

        //act
        var response = await _handler.Handle(request, CancellationToken.None);

        //assert
        Assert.NotNull(response);
        var result = Assert.IsType<GetBugByIdResponse>(response.Data);
        Assert.Equal(request.Id, result.Id);
        Assert.Equal(request.Title, result.Title);
        Assert.Equal(request.Description, result.Description);
        Assert.Equal(request.Status, result.Status);
        if (Bug.IsBugResolvedOrClosed(result.Status))
        {
            Assert.NotNull(result.ResolvedAt);
        }
        else
        {
            Assert.Null(result.ResolvedAt);
        }

        _uow
            .VerifyBeginTransaction(Times.Once())
            .VerifySaveChangesAsync(Times.Once())
            .VerifyCommitTransaction(Times.Once())
            .VerifyRollBackTransaction(Times.Never());

        _uow.BugRepository
            .VerifyGetByIdAsync(Times.Once())
            .VerifyUpdateAsync(Times.Once());
    }
}
