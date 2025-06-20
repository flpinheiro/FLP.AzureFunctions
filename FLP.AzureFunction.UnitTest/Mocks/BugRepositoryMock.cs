using FLP.Core.Context.Main;
using FLP.Core.Context.Query;
using FLP.Core.Interfaces.Repository;
using Moq;

namespace FLP.AzureFunction.UnitTest.Mocks;

internal class BugRepositoryMock
{
    public readonly IBugRepository Object;
    private readonly Mock<IBugRepository> Mock;
    public BugRepositoryMock()
    {
        Mock = new Mock<IBugRepository>(MockBehavior.Strict);
        Object = Mock.Object;
    }
    #region Setupers
    public BugRepositoryMock SetupAddAsync(Bug bug)
    {
        Mock.Setup(x => x.AddAsync(It.IsAny<Bug>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(bug)
            .Verifiable();
        return this;
    }
    public BugRepositoryMock SetupAddAsync(Exception ex)
    {
        Mock.Setup(x => x.AddAsync(It.IsAny<Bug>(), It.IsAny<CancellationToken>()))
            .Throws(() => ex);
        return this;
    }
    public BugRepositoryMock VerifyAddAsync(Times times)
    {
        Mock.Verify(x => x.AddAsync(It.IsAny<Bug>(), It.IsAny<CancellationToken>()), times);
        return this;
    }
    public BugRepositoryMock SetupDeleteAsync()
    {
        Mock.Setup(x => x.DeleteAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask)
            .Verifiable();
        return this;
    }
    public BugRepositoryMock SetupDeleteAsync(Exception ex)
    {
        Mock.Setup(x => x.DeleteAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .Throws(() => ex);
        return this;
    }
    public BugRepositoryMock VerifyDeleteAsync(Times times)
    {
        Mock.Verify(x => x.DeleteAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), times);
        return this;
    }
    public BugRepositoryMock SetupGetByIdAsync(Bug? bug = null)
    {
        Mock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(bug).Verifiable();
        return this;
    }
    public BugRepositoryMock SetupGetByIdAsync(Exception ex)
    {
        Mock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .Throws(() => ex);
        return this;
    }
    public BugRepositoryMock VerifyGetByIdAsync(Times times)
    {
        Mock.Verify(x => x.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), times);
        return this;
    }
    public BugRepositoryMock SetupGetAsync(IEnumerable<Bug> bugs)
    {
        Mock.Setup(x => x.GetAsync(It.IsAny<PaginatedBugQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(bugs).Verifiable();
        return this;
    }
    public BugRepositoryMock SetupGetAsync(Exception ex)
    {
        Mock.Setup(x => x.GetAsync(It.IsAny<PaginatedBugQuery>(), It.IsAny<CancellationToken>()))
            .Throws(() => ex);
        return this;
    }
    public BugRepositoryMock VerifyGetAsync(Times times)
    {
        Mock.Verify(x => x.GetAsync(It.IsAny<PaginatedBugQuery>(), It.IsAny<CancellationToken>()), times);
        return this;
    }
    public BugRepositoryMock SetupCountAsync(int count)
    {
        Mock.Setup(x => x.CountAsync(It.IsAny<PaginatedBugQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(count).Verifiable();
        return this;
    }
    public BugRepositoryMock SetupCountAsync(Exception ex)
    {
        Mock.Setup(x => x.CountAsync(It.IsAny<PaginatedBugQuery>(), It.IsAny<CancellationToken>()))
            .Throws(() => ex);
        return this;
    }
    public BugRepositoryMock VerifyCountAsync(Times times)
    {
        Mock.Verify(x => x.CountAsync(It.IsAny<PaginatedBugQuery>(), It.IsAny<CancellationToken>()), times);
        return this;
    }
    public BugRepositoryMock SetupUpdateAsync(Bug bug)
    {
        Mock.Setup(x => x.UpdateAsync(It.IsAny<Bug>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(bug).Verifiable();
        return this;
    }
    public BugRepositoryMock SetupUpdateAsync(Exception ex)
    {
        Mock.Setup(x => x.UpdateAsync(It.IsAny<Bug>(), It.IsAny<CancellationToken>()))
            .Throws(() => ex);
        return this;
    }
    public BugRepositoryMock VerifyUpdateAsync(Times times)
    {
        Mock.Verify(x => x.UpdateAsync(It.IsAny<Bug>(), It.IsAny<CancellationToken>()), times);
        return this;
    }
    #endregion
}
