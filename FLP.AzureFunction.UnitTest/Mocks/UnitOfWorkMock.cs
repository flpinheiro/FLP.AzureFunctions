using FLP.Core.Interfaces.Repository;
using Moq;

namespace FLP.AzureFunction.UnitTest.Mocks;

internal class UnitOfWorkMock
{
    public IUnitOfWork Object => Mock.Object;
    private Mock<IUnitOfWork> Mock { get; }
    public BugRepositoryMock BugRepository { get; }
    public UnitOfWorkMock()
    {
        Mock = new Mock<IUnitOfWork>(MockBehavior.Strict);
        BugRepository = new BugRepositoryMock();
        Mock.Setup(x => x.BugRepository).Returns(BugRepository.Object);
        Mock.Setup(x => x.BeginTransaction(It.IsAny<CancellationToken>())).Verifiable();
        Mock.Setup(x => x.CommitTransaction(It.IsAny<CancellationToken>())).Verifiable();
        Mock.Setup(x => x.RollbackTransaction()).Verifiable();
        Mock.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);
    }

    #region Setupers
    public UnitOfWorkMock SetupBeginTransaction()
    {
        Mock.Setup(x => x.BeginTransaction(It.IsAny<CancellationToken>())).Verifiable();
        return this;
    }
    public UnitOfWorkMock SetupBeginTransaction(Exception ex)
    {
        Mock.Setup(x => x.BeginTransaction(It.IsAny<CancellationToken>()))
            .Throws(() => ex);
        return this;
    }
    public UnitOfWorkMock VerifyBeginTransaction(Times times)
    {
        Mock.Verify(x => x.BeginTransaction(It.IsAny<CancellationToken>()), times);
        return this;
    }
    public UnitOfWorkMock SetupCommitTransaction()
    {
        Mock.Setup(x => x.CommitTransaction(It.IsAny<CancellationToken>()))
            .Verifiable();
        return this;
    }
    public UnitOfWorkMock SetupCommitTransaction(Exception ex)
    {
        Mock.Setup(x => x.CommitTransaction(It.IsAny<CancellationToken>()))
            .Throws(() => ex);
        return this;
    }
    public UnitOfWorkMock VerifyCommitTransaction(Times times)
    {
        Mock.Verify(x => x.CommitTransaction(It.IsAny<CancellationToken>()), times);
        return this;
    }
    public UnitOfWorkMock SetupRollbackTransaction()
    {
        Mock.Setup(x => x.RollbackTransaction()).Verifiable();
        return this;
    }
    public UnitOfWorkMock VerifyRollBackTransaction(Times times)
    {
        Mock.Verify(x => x.RollbackTransaction(), times);
        return this;
    }
    public UnitOfWorkMock SetupSaveChangesAsync(int result = 1)
    {
        Mock.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(result).Verifiable();
        return this;
    }
    public UnitOfWorkMock SetupSaveChangesAsync(Exception ex)
    {
        Mock.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .Throws(() => ex);
        return this;
    }
    public UnitOfWorkMock VerifySaveChangesAsync(Times times)
    {
        Mock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), times);
        return this;
    }
    #endregion
}
