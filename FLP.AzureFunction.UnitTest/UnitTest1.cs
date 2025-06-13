using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FLP.AzureFunction.UnitTest
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            Assert.True(true);
        }

        [Fact]
        public void Test2()
        {
            var functiont1 = new FLP.AzureFunctions.Function1();

            // Create a mock HttpRequest using DefaultHttpContext
            var context = new DefaultHttpContext();
            var request = context.Request;

            var response = functiont1.Run(request) as OkObjectResult;

            Assert.NotNull(response);

            Assert.Equal("Welcome to Azure Functions!", response.Value);
            Assert.Equal(200, response.StatusCode);
        }
    }
}