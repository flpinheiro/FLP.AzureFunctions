using FLP.Application.Requests.Bugs;
using FLP.Application.Responses.Bugs;
using FLP.AzureFunction.UnitTest.Fixtures;
using FLP.AzureFunction.UnitTest.Fixtures.Requests.Bugs;
using FLP.AzureFunction.UnitTest.Fixtures.Responses.Bugs;
using FLP.AzureFunctions.Functions.Bugs;
using FLP.Core.Context.Shared;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace FLP.AzureFunction.UnitTest.Tests.Functions.Bugs;

public class UpdateBugFunctionTest
{
    private readonly Mock<ILogger<UpdateBugFunction>> _logger = new();
    private readonly Mock<IMediator> _mediator = new();

    private readonly UpdateBugFunction _function;
    public UpdateBugFunctionTest()
    {
        _function = new(_logger.Object, _mediator.Object);
    }

    [Fact]
    public async Task Run_UpdateBugFunction_return_OkObjectResult_Async()
    {
        //arrange
        var createBugRequest = new UpdateBugRequestFixture().Generate();
        var request = HttpRequestFixture.Generate(createBugRequest);
        var GetBug = new GetBugByIdResponseFixture().Generate();
        var result = new BaseResponse<GetBugByIdResponse>(GetBug);

        _mediator
            .Setup(x => x.Send(It.IsAny<UpdateBugRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(result);

        //act
        var response = await _function.RunAsync(request, CancellationToken.None);

        //Assert
        Assert.NotNull(response);
        var ok = Assert.IsType<OkObjectResult>(response);
        var baseResponse = Assert.IsType<BaseResponse<GetBugByIdResponse>>(ok.Value);

        Assert.NotNull(baseResponse);
        Assert.True(baseResponse.IsSuccess);
        Assert.Equal(result.Data, baseResponse.Data);

        _mediator.Verify(x => x.Send(It.IsAny<UpdateBugRequest>(), It.IsAny<CancellationToken>()), Times.Once());
    }
    [Fact]
    public async Task Run_UpdateBugFunction_return_BadRequestObjectResult_Async()
    {
        //arrange
        var createBugRequest = new UpdateBugRequestFixture().Generate();
        var request = HttpRequestFixture.Generate(createBugRequest);
        var GetBug = new GetBugByIdResponseFixture().Generate();
        var result = new BaseResponse<GetBugByIdResponse>(false, "error");

        _mediator
            .Setup(x => x.Send(It.IsAny<UpdateBugRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(result);

        //act
        var response = await _function.RunAsync(request, CancellationToken.None);

        //Assert
        Assert.NotNull(response);
        var ok = Assert.IsType<BadRequestObjectResult>(response);
        var baseResponse = Assert.IsType<BaseResponse<GetBugByIdResponse>>(ok.Value);

        Assert.NotNull(baseResponse);
        Assert.False(baseResponse.IsSuccess);
        Assert.Equal(result.Message, baseResponse.Message);
        
        _mediator.Verify(x => x.Send(It.IsAny<UpdateBugRequest>(), It.IsAny<CancellationToken>()), Times.Once());
    }
    [Fact]
    public async Task Run_UpdateBugFunction_return_BadRequestObjectResult_Validation_Async()
    {
        //arrange
        var request = HttpRequestFixture.Generate();
        var GetBug = new GetBugByIdResponseFixture().Generate();
        var result = new BaseResponse<GetBugByIdResponse>(false, "error");

        _mediator
            .Setup(x => x.Send(It.IsAny<UpdateBugRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(result);

        //act
        var response = await _function.RunAsync(request, CancellationToken.None);

        //Assert
        Assert.NotNull(response);
        var ok = Assert.IsType<BadRequestObjectResult>(response);
        var baseResponse = Assert.IsType<BaseResponse>(ok.Value);

        Assert.NotNull(baseResponse);
        Assert.False(baseResponse.IsSuccess);

        _mediator.Verify(x => x.Send(It.IsAny<UpdateBugRequest>(), It.IsAny<CancellationToken>()), Times.Never());
    }
}
