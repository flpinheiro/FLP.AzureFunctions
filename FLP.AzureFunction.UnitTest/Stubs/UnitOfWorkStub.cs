using FLP.Core.Interfaces.Repository;
using Moq;

namespace FLP.AzureFunction.UnitTest.Stubs;

internal class UnitOfWorkStub 
{
    public IUnitOfWork Object => Stub.Object;
    private Mock<IUnitOfWork> Stub { get; }
    public BugRepositoryStub BugRepository { get; }
    public UnitOfWorkStub()
    {
        Stub = new Mock<IUnitOfWork>(MockBehavior.Strict);
        BugRepository = new BugRepositoryStub();
        Stub.Setup(x => x.BugRepository).Returns(BugRepository.Object);
        Stub.Setup(x => x.BeginTransaction(It.IsAny<CancellationToken>())).Verifiable();
        Stub.Setup(x => x.CommitTransaction(It.IsAny<CancellationToken>())).Verifiable();
        Stub.Setup(x => x.RollbackTransaction()).Verifiable();
        Stub.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);
    }

    #region Setupers
    public UnitOfWorkStub SetupBeginTransaction()
    {
        Stub.Setup(x => x.BeginTransaction(It.IsAny<CancellationToken>())).Verifiable();
        return this;
    }
    public UnitOfWorkStub SetupBeginTransaction(Exception ex)
    {
        Stub.Setup(x => x.BeginTransaction(It.IsAny<CancellationToken>()))
            .Throws(() => ex);
        return this;
    }
    public UnitOfWorkStub VerifyBeginTransaction(Times times)
    {
        Stub.Verify(x => x.BeginTransaction(It.IsAny<CancellationToken>()), times);
        return this;
    }
    public UnitOfWorkStub SetupCommitTransaction()
    {
        Stub.Setup(x => x.CommitTransaction(It.IsAny<CancellationToken>()))
            .Verifiable();
        return this;
    }
    public UnitOfWorkStub SetupCommitTransaction(Exception ex)
    {
        Stub.Setup(x => x.CommitTransaction(It.IsAny<CancellationToken>()))
            .Throws(() => ex);
        return this;
    }
    public UnitOfWorkStub VerifyCommitTransaction(Times times)
    {
        Stub.Verify(x => x.CommitTransaction(It.IsAny<CancellationToken>()), times);
        return this;
    }
    public UnitOfWorkStub SetupRollbackTransaction()
    {
        Stub.Setup(x => x.RollbackTransaction()).Verifiable();
        return this;
    }
    public UnitOfWorkStub VerifyRollBackTransaction(Times times)
    {
        Stub.Verify(x => x.RollbackTransaction(), times);
        return this;
    }
    public UnitOfWorkStub SetupSaveChangesAsync(int result = 1)
    {
        Stub.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(result).Verifiable();
        return this;
    }
    public UnitOfWorkStub SetupSaveChangesAsync(Exception ex)
    {
        Stub.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .Throws(() => ex);
        return this;
    }
    public UnitOfWorkStub VerifySaveChangesAsync(Times times)
    {
        Stub.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), times);
        return this;
    }
    #endregion
}
