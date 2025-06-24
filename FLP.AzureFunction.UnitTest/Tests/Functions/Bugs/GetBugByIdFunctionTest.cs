using Bogus;
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

public class GetBugByIdFunctionTest
{
    private readonly Mock<ILogger<GetBugByIdFunction>> _logger = new();
    private readonly Mock<IMediator> _mediator = new();

    private readonly GetBugByIdFunction _function;

    public GetBugByIdFunctionTest()
    {
        _function = new(_logger.Object, _mediator.Object);
    }

    [Fact]
    public async Task Run_GetBugByIdFunction_return_OkObjectResult_Async()
    {
        //arrange
        var request = HttpRequestFixture.Generate();
        var id = new Faker().Random.Uuid();
        var result = new BaseResponse<GetBugByIdResponse>();

        _mediator
            .Setup(x => x.Send(It.IsAny<GetBugByIdRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(result);

        //act
        var response = await _function.RunAsync(request, id, CancellationToken.None);

        //Assert
        Assert.NotNull(response);
        var ok = Assert.IsType<OkObjectResult>(response);
        var baseResponse = Assert.IsType<BaseResponse<GetBugByIdResponse>>(ok.Value);

        Assert.NotNull(baseResponse);
        Assert.True(baseResponse.IsSuccess);

        _mediator.Verify(x => x.Send(It.IsAny<GetBugByIdRequest>(), It.IsAny<CancellationToken>()), Times.Once());
    }
    [Fact]
    public async Task Run_GetBugByIdFunction_return_BadRequestObjectResult_Async()
    {
        //arrange
        var request = HttpRequestFixture.Generate();
        var id = new Faker().Random.Uuid();
        var result = new BaseResponse<GetBugByIdResponse>(false, "error");

        _mediator
            .Setup(x => x.Send(It.IsAny<GetBugByIdRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(result);

        //act
        var response = await _function.RunAsync(request,id, CancellationToken.None);

        //Assert
        Assert.NotNull(response);
        var ok = Assert.IsType<BadRequestObjectResult>(response);
        var baseResponse = Assert.IsType<BaseResponse<GetBugByIdResponse>>(ok.Value);

        Assert.NotNull(baseResponse);
        Assert.False(baseResponse.IsSuccess);
        Assert.Equal(result.Message, baseResponse.Message);

        _mediator.Verify(x => x.Send(It.IsAny<GetBugByIdRequest>(), It.IsAny<CancellationToken>()), Times.Once());
    }
}