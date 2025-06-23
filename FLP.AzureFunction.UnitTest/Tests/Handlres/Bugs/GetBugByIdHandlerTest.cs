using AutoMapper;
using FLP.Application.Handlers.Bugs;
using FLP.Application.Profiles;
using FLP.Application.Responses.Bugs;
using FLP.AzureFunction.UnitTest.Fixtures.Models;
using FLP.AzureFunction.UnitTest.Fixtures.Requests.Bugs;
using FLP.AzureFunction.UnitTest.Mocks;
using FLP.AzureFunction.UnitTest.Stubs;
using FLP.Core.Exceptions;
using Microsoft.Extensions.Logging;
using Moq;

namespace FLP.AzureFunction.UnitTest.Tests.Handlres.Bugs;

public class GetBugByIdHandlerTest
{
    private readonly GetBugByIdHandler _handler;
    private readonly UnitOfWorkMock _uow = new();
    private readonly IMapper _mapper;
    private readonly Mock<ILogger<GetBugByIdHandler>> _logger = new();
    public GetBugByIdHandlerTest()
    {
        _mapper = IMapperStub.GetMapper(new BugProfile());
        // Initialize the handler with necessary dependencies
        _handler = new GetBugByIdHandler(_uow.Object, _mapper, _logger.Object);
    }

    [Fact]
    public async Task Run_GetBugById_Returns_Async()
    {
        // Arrange
        var request = new GetBugByIdRequestFixture().Generate();
        var bug = new BugFixture().WithId(request.Id).Generate();

        _uow.BugRepository.SetupGetByIdAsync(bug);

        // Act
        var response = await _handler.Handle(request, CancellationToken.None);

        //// Assert
        Assert.NotNull(response);
        var result = Assert.IsType<GetBugByIdResponse>(response.Data);
        Assert.Equal(request.Id, result.Id);
        Assert.Equal(bug.Title, result.Title);
        Assert.Equal(bug.Description, result.Description);
        Assert.Equal(bug.AssignedToUserId, result.AssignedToUserId);
        Assert.Equal(bug.Status, result.Status);
        Assert.Equal(bug.ResolvedAt, result.ResolvedAt);

        _uow.BugRepository.VerifyGetByIdAsync(Times.Once());
    }

    [Fact]
    public async Task Run_GetBugById_Throw_ArguimentException_Async()
    {
        // Arrange
        var request = new GetBugByIdRequestFixture().WithId(Guid.Empty).Generate();

        // Act
        var result = await  _handler.Handle(request, CancellationToken.None);

        //// Assert
        Assert.NotNull(result);
        Assert.False(result.IsSuccess);
        Assert.NotNull(result.Message);

        _uow.BugRepository.VerifyGetByIdAsync(Times.Never());
    }

    [Fact]
    public async Task Run_GetBugById_Throw_NotFoundException_Async()
    {
        // Arrange
        var request = new GetBugByIdRequestFixture().Generate();

        _uow.BugRepository.SetupGetByIdAsync();

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        //// Assert
        Assert.NotNull(result);
        Assert.False(result.IsSuccess);
        Assert.NotNull(result.Message);

        _uow.BugRepository.VerifyGetByIdAsync(Times.Once());
    }
}
