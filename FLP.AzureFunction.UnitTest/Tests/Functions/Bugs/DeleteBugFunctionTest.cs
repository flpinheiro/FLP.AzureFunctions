using Bogus;
using FLP.Application.Requests.Bugs;
using FLP.AzureFunction.UnitTest.Fixtures;
using FLP.AzureFunction.UnitTest.Fixtures.Responses.Bugs;
using FLP.AzureFunctions.Functions.Bugs;
using FLP.Core.Context.Shared;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace FLP.AzureFunction.UnitTest.Tests.Functions.Bugs;

public class DeleteBugFunctionTest
{
    private readonly Mock<ILogger<DeleteBugFunction>> _logger = new();
    private readonly Mock<IMediator> _mediator = new();

    private readonly DeleteBugFunction _function;

    public DeleteBugFunctionTest()
    {
        _function = new DeleteBugFunction(_logger.Object, _mediator.Object);
    }

    [Fact]
    public async Task Run_DeleteBugFunction_return_OkObjectResult_Async()
    {
        //arrange
        var id = new Faker().Random.Uuid();
        var request = HttpRequestFixture.Generate();
        var GetBug = new GetBugByIdResponseFixture().Generate();
        var result = new BaseResponse(true, "test success");

        _mediator
            .Setup(x => x.Send(It.IsAny<DeleteBugRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(result);

        //act
        var response = await _function.RunAsync(request,id, CancellationToken.None);

        //Assert
        Assert.NotNull(response);
        var ok = Assert.IsType<OkObjectResult>(response);
        var baseResponse = Assert.IsType<BaseResponse>(ok.Value);

        Assert.NotNull(baseResponse);
        Assert.True(baseResponse.IsSuccess);
        Assert.Equal(result.Message, baseResponse.Message);

        _mediator.Verify(x => x.Send(It.IsAny<DeleteBugRequest>(), It.IsAny<CancellationToken>()), Times.Once());
    }

    [Fact]
    public async Task Run_DeleteBugFunction_return_BadRequestObjectResult_Async()
    {
        //arrange
        var id = new Faker().Random.Uuid();
        var request = HttpRequestFixture.Generate();
        var GetBug = new GetBugByIdResponseFixture().Generate();
        var result = new BaseResponse(false, "test fail");

        _mediator
            .Setup(x => x.Send(It.IsAny<DeleteBugRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(result);

        //act
        var response = await _function.RunAsync(request, id, CancellationToken.None);

        //Assert
        Assert.NotNull(response);
        var ok = Assert.IsType<BadRequestObjectResult>(response);
        var baseResponse = Assert.IsType<BaseResponse>(ok.Value);

        Assert.NotNull(baseResponse);
        Assert.False(baseResponse.IsSuccess);
        Assert.Equal(result.Message, baseResponse.Message);

        _mediator.Verify(x => x.Send(It.IsAny<DeleteBugRequest>(), It.IsAny<CancellationToken>()), Times.Once());
    }
}
