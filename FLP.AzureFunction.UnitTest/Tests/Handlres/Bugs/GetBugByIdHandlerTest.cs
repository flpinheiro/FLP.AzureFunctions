using AutoMapper;
using FLP.Application.Handlers.Bugs;
using FLP.Application.Profiles;
using FLP.AzureFunction.UnitTest.Mocks.Models;
using FLP.AzureFunction.UnitTest.Mocks.Requests.Bugs;
using FLP.AzureFunction.UnitTest.Stubs;
using FLP.Core.Exceptions;
using Microsoft.Extensions.Logging;
using Moq;

namespace FLP.AzureFunction.UnitTest.Tests.Handlres.Bugs;

public class GetBugByIdHandlerTest
{
    private readonly GetBugByIdHandler _handler;
    private readonly UnitOfWorkStub _stubs = new UnitOfWorkStub();
    private readonly IMapper _mapper;
    private readonly Mock<ILogger<GetBugByIdHandler>> _logger = new Mock<ILogger<GetBugByIdHandler>>();
    public GetBugByIdHandlerTest()
    {
        _mapper = IMapperStub.GetMapper(new BugProfile());
        // Initialize the handler with necessary dependencies
        _handler = new GetBugByIdHandler(_stubs.Object, _mapper, _logger.Object);
    }

    [Fact]
    public async Task Run_GetBugById_Returns_Async()
    {
        // Arrange
        var request = new GetBugByIdRequestMock().Generate();
        var bug = new BugMock().WithId(request.Id).Generate();

        _stubs.BugRepository.SetupGetByIdAsync(bug);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        //// Assert
        Assert.NotNull(result);
        Assert.Equal(request.Id, result.Id);
        Assert.Equal(bug.Title, result.Title);
        Assert.Equal(bug.Description, result.Description);
        Assert.Equal(bug.AssignedToUserId, result.AssignedToUserId);
        Assert.Equal(bug.Status, result.Status);
        Assert.Equal(bug.ResolvedAt, result.ResolvedAt);

        _stubs.BugRepository.VerifyGetByIdAsync(Times.Once());
    }

    [Fact]
    public async Task Run_GetBugById_Throw_ArguimentException_Async()
    {
        // Arrange
        var request = new GetBugByIdRequestMock().WithId(Guid.Empty).Generate();

        // Act
        var result = await Assert.ThrowsAsync<ArgumentException>(() => _handler.Handle(request, CancellationToken.None));

        //// Assert
        Assert.NotNull(result);
        Assert.NotEmpty(result.Message);

        _stubs.BugRepository.VerifyGetByIdAsync(Times.Never());
    }

    [Fact]
    public async Task Run_GetBugById_Throw_NotFoundException_Async()
    {
        // Arrange
        var request = new GetBugByIdRequestMock().Generate();

        _stubs.BugRepository.SetupGetByIdAsync();

        // Act
        var result = await Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(request, CancellationToken.None));

        //// Assert
        Assert.NotNull(result);
        Assert.NotEmpty(result.Message);
        Assert.Equal("Bug not found", result.Message);

        _stubs.BugRepository.VerifyGetByIdAsync(Times.Once());
    }
}
