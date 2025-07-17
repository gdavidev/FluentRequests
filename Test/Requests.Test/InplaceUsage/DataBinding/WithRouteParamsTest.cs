using DaveCommonsSoftware.Lib.Requests.Builder;
using DaveCommonsSoftware.Lib.Requests.Data;
using Requests.Test.Mock.Mocks;
using System.Net;

namespace Requests.Test.InplaceUsage.DataBinding
{
    public class WithRouteParamsTest
    {
        [Fact]
        public async Task WithRouteParams_Primitive_ParamAddedToTheUriPath()
        {
            // - Arrange ------
            var mockedMessageHandler = HttpClientMock.GetMockedHttpMessageHandler("", HttpStatusCode.OK);
            var mockedHttpClient = new HttpClient(mockedMessageHandler.Object);

            var targetUri = "https://example.com/product/{id}";
            var productId = 16;

            // - Act ----------
            RequestsResult<string> result = await RequestBuilder
                .Post<string>(targetUri, context => context.HttpRequestClient = mockedHttpClient)
                .WithRouteParams(productId)
                .SendAsync();

            // - Assert -------
            Assert.Equal("https://example.com/product/{id}", result.Context.Uri);
            Assert.Equal($"https://example.com/product/{productId}", result.FinalUri);
        }

        [Fact]
        public async Task WithRouteParams_AnonymousObject_ParamAddedToTheUriPath()
        {
            // - Arrange ------
            var mockedMessageHandler = HttpClientMock.GetMockedHttpMessageHandler("", HttpStatusCode.OK);
            var mockedHttpClient = new HttpClient(mockedMessageHandler.Object);

            var targetUri = "https://example.com/product/{id}";
            var productId = 16;

            // - Act ----------
            RequestsResult<string> result = await RequestBuilder
                .Post<string>(targetUri, context => context.HttpRequestClient = mockedHttpClient)
                .WithRouteParams(new { Id = productId })
                .SendAsync();

            // - Assert -------
            Assert.Equal("https://example.com/product/{id}", result.Context.Uri);
            Assert.Equal($"https://example.com/product/{productId}", result.FinalUri);
        }

        [Fact]
        public async Task WithRouteParams_MultipleParamsWithAnonymousObject_ParamAddedToTheUriPath()
        {
            // - Arrange ------
            var mockedMessageHandler = HttpClientMock.GetMockedHttpMessageHandler("", HttpStatusCode.OK);
            var mockedHttpClient = new HttpClient(mockedMessageHandler.Object);

            var targetUri = "https://example.com/product/{warehouseId}/{id}";
            var warehouseId = 2;
            var productId = 16;

            // - Act ----------
            RequestsResult<string> result = await RequestBuilder
                .Post<string>(targetUri, context => context.HttpRequestClient = mockedHttpClient)
                .WithRouteParams(new { WarehouseId = warehouseId, Id = productId })
                .SendAsync();

            // - Assert -------
            Assert.Equal("https://example.com/product/{warehouseId}/{id}", result.Context.Uri);
            Assert.Equal($"https://example.com/product/{warehouseId}/{productId}", result.FinalUri);
        }

        [Fact]
        public async Task WithRouteParams_MultipleTypedParamsWithAnonymousObject_ParamAddedToTheUriPath()
        {
            // - Arrange ------
            var mockedMessageHandler = HttpClientMock.GetMockedHttpMessageHandler("", HttpStatusCode.OK);
            var mockedHttpClient = new HttpClient(mockedMessageHandler.Object);

            var targetUri = "https://example.com/product/{warehouseId:int}/{id:int}";
            var warehouseId = 2;
            var productId = 16;

            // - Act ----------
            RequestsResult<string> result = await RequestBuilder
                .Post<string>(targetUri, context => context.HttpRequestClient = mockedHttpClient)
                .WithRouteParams(new { WarehouseId = warehouseId, Id = productId })
                .SendAsync();

            // - Assert -------
            Assert.Equal("https://example.com/product/{warehouseId:int}/{id:int}", result.Context.Uri);
            Assert.Equal($"https://example.com/product/{warehouseId}/{productId}", result.FinalUri);
        }
    }
}
