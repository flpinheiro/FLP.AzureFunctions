using FLP.Application.Requests.Bugs;
using FLP.Application.Responses.Bugs;
using FLP.AzureFunction.UnitTest.Fixtures;
using FLP.AzureFunctions.Functions.Bugs;
using FLP.Core.Context.Shared;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace FLP.AzureFunction.UnitTest.Tests.Functions.Bugs;

public class GetBugsPaginatedFunctionTest
{
    private readonly Mock<ILogger<GetBugsPaginatedFunction>> _logger = new();
    private readonly Mock<IMediator> _mediator = new();

    private readonly GetBugsPaginatedFunction _function;

    public GetBugsPaginatedFunctionTest()
    {
        _function = new(_logger.Object, _mediator.Object);
    }

    [Fact]
    public async Task Run_GetBugsPaginatedFunction_return_OkObjectResult_Async()
    {
        //arrange
        var request = HttpRequestFixture.Generate();
        var result = new BaseResponse<GetBugsPaginatedResponse>();

        _mediator
            .Setup(x => x.Send(It.IsAny<GetBugsPaginatedRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(result);

        //act
        var response = await _function.RunAsync(request, CancellationToken.None);

        //Assert
        Assert.NotNull(response);
        var ok = Assert.IsType<OkObjectResult>(response);
        var baseResponse = Assert.IsType<BaseResponse<GetBugsPaginatedResponse>>(ok.Value);

        Assert.NotNull(baseResponse);
        Assert.True(baseResponse.IsSuccess);

        _mediator.Verify(x => x.Send(It.IsAny<GetBugsPaginatedRequest>(), It.IsAny<CancellationToken>()), Times.Once());
    }
    [Fact]
    public async Task Run_GetBugsPaginatedFunction_return_BadRequestObjectResult_Async()
    {
        //arrange
        var request = HttpRequestFixture.Generate();
        var result = new BaseResponse<GetBugsPaginatedResponse>(false, "error");

        _mediator
            .Setup(x => x.Send(It.IsAny<GetBugsPaginatedRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(result);

        //act
        var response = await _function.RunAsync(request, CancellationToken.None);

        //Assert
        Assert.NotNull(response);
        var ok = Assert.IsType<BadRequestObjectResult>(response);
        var baseResponse = Assert.IsType<BaseResponse<GetBugsPaginatedResponse>>(ok.Value);

        Assert.NotNull(baseResponse);
        Assert.False(baseResponse.IsSuccess);
        Assert.Equal(result.Message, baseResponse.Message);

        _mediator.Verify(x => x.Send(It.IsAny<GetBugsPaginatedRequest>(), It.IsAny<CancellationToken>()), Times.Once());
    }
}
