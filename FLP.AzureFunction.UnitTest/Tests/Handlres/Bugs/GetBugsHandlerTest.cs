using AutoMapper;

using FLP.Application.Handlers.Bugs;
using FLP.Application.Profiles;
using FLP.Application.Requests.Bugs;
using FLP.Application.Responses.Bugs;
using FLP.AzureFunction.UnitTest.Fixtures.Models;
using FLP.AzureFunction.UnitTest.Mocks;
using FLP.AzureFunction.UnitTest.Stubs;
using Microsoft.Extensions.Logging;
using Moq;

namespace FLP.AzureFunction.UnitTest.Tests.Handlres.Bugs;

public class GetBugsHandlerTest
{
    private readonly UnitOfWorkMock _uow = new();
    private readonly IMapper _mapper;
    private readonly Mock<ILogger<GetBugsHandler>> _logger = new();

    private readonly GetBugsHandler _handler;
    public GetBugsHandlerTest()
    {
        _mapper = IMapperStub.GetMapper(new BugProfile());
        _handler = new GetBugsHandler(_uow.Object, _mapper, _logger.Object);
    }

    [Theory]
    [InlineData(10)]
    [InlineData(0)]
    public async Task Run_GetPaginatedBug_Async(int count)
    {
        // Arrange
        var request = new GetBugsRequest();
        var bugs = new BugFixture().Generate(count);

        _uow.BugRepository.SetupGetAsync(bugs);
        _uow.BugRepository.SetupCountAsync(count);

        // Act
        var response = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(response);
        var result = Assert.IsType<GetBugsResponse>(response.Data);
        Assert.Equal(count, result.Total);
        Assert.NotNull(result.Bugs);
        Assert.Equal(count, result.Bugs.Count());
        _uow.BugRepository.VerifyGetAsync(Times.Once());
        _uow.BugRepository.VerifyCountAsync(Times.Once());
    }
}
