using AutoMapper;

using FLP.Application.Handlers.Bugs;
using FLP.Application.Profiles;
using FLP.Application.Requests.Bugs;
using FLP.AzureFunction.UnitTest.Mocks.Models;
using FLP.AzureFunction.UnitTest.Stubs;
using Microsoft.Extensions.Logging;
using Moq;

namespace FLP.AzureFunction.UnitTest.Tests.Handlres.Bugs;

public class GetBugsHandlerTest
{
    private readonly UnitOfWorkStub _stub = new();
    private readonly IMapper _mapper;
    private readonly Mock<ILogger<GetBugsHandler>> _logger = new();

    private readonly GetBugsHandler _handler;
    public GetBugsHandlerTest()
    {
        _mapper = IMapperStub.GetMapper(new BugProfile());
        _handler = new GetBugsHandler(_stub.Object, _mapper, _logger.Object);
    }

    [Theory]
    [InlineData(10)]
    [InlineData(0)]
    public async Task Run_GetPaginatedBug_Async(int count)
    {
        // Arrange
        var request = new GetBugsRequest();
        var bugs = new BugMock().Generate(count);

        _stub.BugRepository.SetupGetAsync(bugs);
        _stub.BugRepository.SetupCountAsync(count);

        // Act
        var result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(count, result.Total);
        _stub.BugRepository.VerifyGetAsync(Times.Once());
        _stub.BugRepository.VerifyCountAsync(Times.Once());
    }
}
