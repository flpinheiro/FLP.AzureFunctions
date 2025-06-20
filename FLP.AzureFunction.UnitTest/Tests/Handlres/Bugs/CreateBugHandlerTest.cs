using AutoMapper;
using FLP.Application.Handlers.Bugs;
using FLP.Application.Profiles;
using FLP.Application.Responses.Bugs;
using FLP.AzureFunction.UnitTest.Fixtures.Models;
using FLP.AzureFunction.UnitTest.Fixtures.Requests.Bugs;
using FLP.AzureFunction.UnitTest.Mocks;
using FLP.AzureFunction.UnitTest.Stubs;
using Microsoft.Extensions.Logging;
using Moq;

namespace FLP.AzureFunction.UnitTest.Tests.Handlres.Bugs;

public class CreateBugHandlerTest
{
    private readonly UnitOfWorkMock _uow = new();
    private readonly Mock<ILogger<CreateBugHandler>> _logger = new();
    private readonly IMapper _mapper;

    private readonly CreateBugHandler _handler;

    public CreateBugHandlerTest()
    {
        _mapper = IMapperStub.GetMapper(
            new BugProfile()
        );
        _handler = new CreateBugHandler(_uow.Object, _mapper, _logger.Object);
    }

    [Fact]
    public async Task Run_CreateBugHandler_return_OK_Async()
    {
        //arrange
        var request = new CreateBugRequestFixture().Generate();
        var bug = new BugFixture().Generate();

        _uow.BugRepository
            .SetupAddAsync(bug);

        _uow
            .SetupBeginTransaction()
            .SetupCommitTransaction()
            .SetupSaveChangesAsync();

        //act
        var response = await _handler.Handle(request, CancellationToken.None);

        //assert
        Assert.NotNull(response);
        var result = Assert.IsType<GetBugByIdResponse>(response.Data);
        Assert.Equal(bug.Title, result.Title);
        Assert.Equal(bug.Description, result.Description);
        Assert.Equal(bug.Id, result.Id);
        Assert.Equal(bug.AssignedToUserId, result.AssignedToUserId);
        Assert.Equal(bug.Status, result.Status);

        _uow.BugRepository.VerifyAddAsync(Times.Once());
        _uow
            .VerifyBeginTransaction(Times.Once())
            .VerifyCommitTransaction(Times.Once())
            .VerifySaveChangesAsync(Times.Once())
            .VerifyRollBackTransaction(Times.Never());
    }

    [Fact]
    public async Task Run_CreateBugHandler_Validation_Fail_Async()
    {
        //arrange
        var request = new CreateBugRequestFixture()
            .WithTitle(null) // Invalid title
            .WithDescription(null) // Invalid description
            .Generate();

        //act
        var response = await Assert.ThrowsAsync<ArgumentException>(() => _handler.Handle(request, CancellationToken.None));

        //assert
        Assert.NotNull(response);
        Assert.NotEmpty(response.Message);

        _uow.BugRepository.VerifyAddAsync(Times.Never());
        _uow
            .VerifyBeginTransaction(Times.Never())
            .VerifyCommitTransaction(Times.Never())
            .VerifySaveChangesAsync(Times.Never())
            .VerifyRollBackTransaction(Times.Never());
    }

    [Fact]
    public async Task Run_CreateBugHandler_Throw_Exception_Async()
    {
        //arrange
        var request = new CreateBugRequestFixture().Generate();

        _uow.BugRepository
            .SetupAddAsync(new Exception("Test Excption"));

        _uow
            .SetupBeginTransaction()
            .SetupRollbackTransaction();

        //act
        var response = await Assert.ThrowsAsync<Exception>(() => _handler.Handle(request, CancellationToken.None));

        //assert
        Assert.NotNull(response);
        Assert.IsType<Exception>(response);
        Assert.NotEmpty(response.Message);
        Assert.NotNull(response.InnerException);


        _uow.BugRepository.VerifyAddAsync(Times.Once());
        _uow
            .VerifyBeginTransaction(Times.Once())
            .VerifyCommitTransaction(Times.Never())
            .VerifySaveChangesAsync(Times.Never())
            .VerifyRollBackTransaction(Times.Once());
    }
}
