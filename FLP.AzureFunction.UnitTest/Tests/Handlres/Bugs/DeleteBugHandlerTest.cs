using FLP.Application.Handlers.Bugs;
using FLP.Application.Requests.Bugs;
using FLP.AzureFunction.UnitTest.Mocks;
using Microsoft.Extensions.Logging;
using Moq;

namespace FLP.AzureFunction.UnitTest.Tests.Handlres.Bugs;

public class DeleteBugHandlerTest
{
    private readonly UnitOfWorkMock _uow = new();
    //private readonly IMapper _mapper;
    private readonly Mock<ILogger<DeleteBugHandler>> _logger = new();

    private readonly DeleteBugHandler _handler;

    public DeleteBugHandlerTest()
    {
        //_mapper = IMapperStub.GetMapper(new BugProfile());
        _handler = new DeleteBugHandler(_uow.Object, /*_mapper,*/ _logger.Object);
    }

    [Fact]
    public async Task Run_DeleteBug_Async()
    {
        // Arrange
        var bugId = Guid.NewGuid();
        _uow.BugRepository.SetupDeleteAsync();

        _uow.SetupBeginTransaction()
            .SetupSaveChangesAsync()
            .SetupCommitTransaction();

        // Act
        await _handler.Handle(new DeleteBugRequest(bugId), CancellationToken.None);

        // Assert
        _uow.VerifyBeginTransaction(Times.Once());
        _uow.VerifyCommitTransaction(Times.Once());
        _uow.VerifySaveChangesAsync(Times.Once());
        _uow.VerifyRollBackTransaction(Times.Never());

        _uow.BugRepository.VerifyDeleteAsync(Times.Once());
    }

    [Theory]
    [InlineData(typeof(Exception))]
    [InlineData(typeof(KeyNotFoundException))]
    public async Task Run_DeleteBug_Throw_Exception_Async(Type type)
    {
        // Arrange
        var ex = type.GetConstructor(Type.EmptyTypes)?.Invoke(null) as Exception ?? throw new InvalidOperationException("Could not create exception instance.");
        var bugId = Guid.NewGuid();
        
        _uow.BugRepository.SetupDeleteAsync(ex);

        _uow.SetupBeginTransaction()
            .SetupRollbackTransaction();

        // Act
        await Assert.ThrowsAsync(type, () => _handler.Handle(new DeleteBugRequest(bugId), CancellationToken.None));

        // Assert
        _uow.VerifyBeginTransaction(Times.Once());
        _uow.VerifyCommitTransaction(Times.Never());
        _uow.VerifySaveChangesAsync(Times.Never());
        _uow.VerifyRollBackTransaction(Times.Once());

        _uow.BugRepository.VerifyDeleteAsync(Times.Once());
    }
}
