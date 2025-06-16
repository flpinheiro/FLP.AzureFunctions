using FLP.Core.Context.Main;
using FLP.Core.Interfaces.Repository;
using Moq;

namespace FLP.AzureFunction.UnitTest.Stubs;

internal class BugRepositoryStub
{
    public readonly IBugRepository Object;
    private readonly Mock<IBugRepository> Stub;
    public BugRepositoryStub()
    {
        Stub = new Mock<IBugRepository>(MockBehavior.Strict);
        Object = Stub.Object;
    }
    #region Setupers
    public BugRepositoryStub SetupAddAsync(Bug bug)
    {
        Stub.Setup(x => x.AddAsync(It.IsAny<Bug>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(bug)
            .Verifiable();
        return this;
    }
    public BugRepositoryStub SetupAddAsync(Exception ex )
    {
        Stub.Setup(x => x.AddAsync(It.IsAny<Bug>(), It.IsAny<CancellationToken>()))
            .Throws(() => ex);
        return this;
    }
    public BugRepositoryStub VerifyAddAsync(Times times)
    {
        Stub.Verify(x => x.AddAsync(It.IsAny<Bug>(), It.IsAny<CancellationToken>()), times);
        return this;
    }
    public BugRepositoryStub SetupDeleteAsync()
    {
        Stub.Setup(x => x.DeleteAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask)
            .Verifiable();
        return this;
    }
    public BugRepositoryStub SetupDeleteAsync(Exception ex)
    {
        Stub.Setup(x => x.DeleteAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .Throws(() => ex);
        return this;
    }
    public BugRepositoryStub VerifyDeleteAsync(Times times)
    {
        Stub.Verify(x => x.DeleteAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), times);
        return this;
    }
    public BugRepositoryStub SetupGetByIdAsync(Bug bug)
    {
        Stub.Setup(x => x.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(bug).Verifiable();
        return this;
    }
    public BugRepositoryStub SetupGetByIdAsync(Exception ex)
    {
        Stub.Setup(x => x.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .Throws(() => ex);
        return this;
    }
    public BugRepositoryStub VerifyGetByIdAsync(Times times)
    {
        Stub.Verify(x => x.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), times);
        return this;
    }
    public BugRepositoryStub SetupGetAllAsync(IEnumerable<Bug> bugs)
    {
        Stub.Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(bugs).Verifiable();
        return this;
    }
    public BugRepositoryStub SetupGetAllAsync(Exception ex)
    {
        Stub.Setup(x => x.GetAllAsync(It.IsAny<CancellationToken>()))
            .Throws(() => ex);
        return this;
    }
    public BugRepositoryStub VerifyGetAllAsync(Times times)
    {
        Stub.Verify(x => x.GetAllAsync(It.IsAny<CancellationToken>()), times);
        return this;
    }
    public BugRepositoryStub SetupGetByStatusAsync(IEnumerable<Bug> bugs)
    {
        Stub.Setup(x => x.GetByStatusAsync(It.IsAny<FLP.Core.Context.Constants.BugStatus>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(bugs).Verifiable();
        return this;
    }
    public BugRepositoryStub SetupGetByStatusAsync(Exception ex)
    {
        Stub.Setup(x => x.GetByStatusAsync(It.IsAny<FLP.Core.Context.Constants.BugStatus>(), It.IsAny<CancellationToken>()))
            .Throws(() => ex);
        return this;
    }
    public BugRepositoryStub VerifyGetByStatusAsync(Times times)
    {
        Stub.Verify(x => x.GetByStatusAsync(It.IsAny<FLP.Core.Context.Constants.BugStatus>(), It.IsAny<CancellationToken>()), times);
        return this;
    }
    public BugRepositoryStub SetupUpdateAsync(Bug bug)
    {
        Stub.Setup(x => x.UpdateAsync(It.IsAny<Bug>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(bug).Verifiable();
        return this;
    }
    public BugRepositoryStub SetupUpdateAsync(Exception ex)
    {
        Stub.Setup(x => x.UpdateAsync(It.IsAny<Bug>(), It.IsAny<CancellationToken>()))
            .Throws(() => ex);
        return this;
    }
    public BugRepositoryStub VerifyUpdateAsync(Times times)
    {
        Stub.Verify(x => x.UpdateAsync(It.IsAny<Bug>(), It.IsAny<CancellationToken>()), times);
        return this;
    }
    #endregion
}
