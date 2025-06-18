using FLP.Application.Handlers.Bugs;
using FLP.Application.Requests.Bugs;
using FLP.AzureFunction.UnitTest.Stubs;
using Microsoft.Extensions.Logging;
using Moq;

namespace FLP.AzureFunction.UnitTest.Tests.Handlres.Bugs;

public class DeleteBugHandlerTest
{
    private readonly UnitOfWorkStub _stub = new();
    //private readonly IMapper _mapper;
    private readonly Mock<ILogger<DeleteBugHandler>> _logger = new();

    private readonly DeleteBugHandler _handler;

    public DeleteBugHandlerTest()
    {
        //_mapper = IMapperStub.GetMapper(new BugProfile());
        _handler = new DeleteBugHandler(_stub.Object, /*_mapper,*/ _logger.Object);
    }

    [Fact]
    public async Task Run_DeleteBug_Async()
    {
        // Arrange
        var bugId = Guid.NewGuid();
        _stub.BugRepository.SetupDeleteAsync();

        _stub.SetupBeginTransaction()
            .SetupSaveChangesAsync()
            .SetupCommitTransaction();

        // Act
        await _handler.Handle(new DeleteBugRequest(bugId), CancellationToken.None);

        // Assert
        _stub.VerifyBeginTransaction(Times.Once());
        _stub.VerifyCommitTransaction(Times.Once());
        _stub.VerifySaveChangesAsync(Times.Once());
        _stub.VerifyRollBackTransaction(Times.Never());

        _stub.BugRepository.VerifyDeleteAsync(Times.Once());
    }

    [Theory]
    [InlineData(typeof(Exception))]
    [InlineData(typeof(KeyNotFoundException))]
    public async Task Run_DeleteBug_Throw_Exception_Async(Type type)
    {
        // Arrange
        var ex = type.GetConstructor(Type.EmptyTypes)?.Invoke(null) as Exception;
        if (ex == null) throw new InvalidOperationException("Could not create exception instance.");
        
        var bugId = Guid.NewGuid();
        
        _stub.BugRepository.SetupDeleteAsync(ex);

        _stub.SetupBeginTransaction()
            .SetupRollbackTransaction();

        // Act
        await Assert.ThrowsAsync(type, () => _handler.Handle(new DeleteBugRequest(bugId), CancellationToken.None));

        // Assert
        _stub.VerifyBeginTransaction(Times.Once());
        _stub.VerifyCommitTransaction(Times.Never());
        _stub.VerifySaveChangesAsync(Times.Never());
        _stub.VerifyRollBackTransaction(Times.Once());

        _stub.BugRepository.VerifyDeleteAsync(Times.Once());
    }
}
