using AutoMapper;
using FLP.Application.Handlers.Bugs;
using FLP.Application.Profiles;
using FLP.Application.Responses.Bugs;
using FLP.AzureFunction.UnitTest.Mocks.Models;
using FLP.AzureFunction.UnitTest.Mocks.Requests.Bugs;
using FLP.AzureFunction.UnitTest.Stubs;
using Microsoft.Extensions.Logging;
using Moq;

namespace FLP.AzureFunction.UnitTest.Tests.Handlres.Bugs;

public class CreateBugHandlerTest 
{
    private readonly UnitOfWorkStub _stubs = new UnitOfWorkStub();
    private readonly Mock<ILogger<CreateBugHandler>> _logger = new Mock<ILogger<CreateBugHandler>>();
    private readonly IMapper _mapper;

    private readonly CreateBugHandler _handler;

    public CreateBugHandlerTest()
    {
        _mapper = IMapperStub.GetMapper(
            new BugProfile()
        );
        _handler =new CreateBugHandler(_stubs.Object, _mapper, _logger.Object);
    }

    [Fact]
    public async Task Run_CreateBugHandler_return_OK_Async()
    {
        //arrange
        var request = new CreateBugRequestMock().Generate();
        var bug = new BugMock().Generate();

        _stubs.BugRepository
            .SetupAddAsync(bug);

        _stubs
            .SetupBeginTransaction()
            .SetupCommitTransaction()
            .SetupSaveChangesAsync();

        //act
        var response = await _handler.Handle(request, CancellationToken.None);

        //assert
        Assert.NotNull(response);
        Assert.IsType<CreateBugReponse>(response);
        Assert.Equal(bug.Title, response.Title);
        Assert.Equal(bug.Description, response.Description);
        Assert.Equal(bug.Id, response.Id);
        Assert.Equal(bug.AssignedToUserId, response.AssignedToUserId);
        Assert.Equal(bug.Status, response.Status);

        _stubs.BugRepository.VerifyAddAsync(Times.Once());
        _stubs
            .VerifyBeginTransaction(Times.Once())
            .VerifyCommitTransaction(Times.Once())
            .VerifySaveChangesAsync(Times.Once())
            .VerifyRollBackTransaction(Times.Never());
    }

    [Fact]
    public async Task Run_CreateBugHandler_Validation_Fail_Async()
    {
        //arrange
        var request = new CreateBugRequestMock()
            .WithTitle(null) // Invalid title
            .WithDescription(null) // Invalid description
            .Generate();

        //act
        var response = await Assert.ThrowsAsync<ArgumentException>(() => _handler.Handle(request, CancellationToken.None));

        //assert
        Assert.NotNull(response);
        Assert.NotEmpty(response.Message);

        _stubs.BugRepository.VerifyAddAsync(Times.Never());
        _stubs
            .VerifyBeginTransaction(Times.Never())
            .VerifyCommitTransaction(Times.Never())
            .VerifySaveChangesAsync(Times.Never())
            .VerifyRollBackTransaction(Times.Never());
    }

    [Fact]
    public async Task Run_CreateBugHandler_Throw_Exception_Async()
    {
        //arrange
        var request = new CreateBugRequestMock().Generate();

        _stubs.BugRepository
            .SetupAddAsync(new Exception("Test Excption"));

        _stubs
            .SetupBeginTransaction()
            .SetupRollbackTransaction();

        //act
        var response = await Assert.ThrowsAsync<Exception>(() => _handler.Handle(request, CancellationToken.None));

        //assert
        Assert.NotNull(response);
        Assert.IsType<Exception>(response);
        Assert.NotEmpty(response.Message);
        Assert.NotNull(response.InnerException);


        _stubs.BugRepository.VerifyAddAsync(Times.Once());
        _stubs
            .VerifyBeginTransaction(Times.Once())
            .VerifyCommitTransaction(Times.Never())
            .VerifySaveChangesAsync(Times.Never())
            .VerifyRollBackTransaction(Times.Once());
    }
}
