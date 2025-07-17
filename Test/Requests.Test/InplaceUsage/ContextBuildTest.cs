using DaveCommonsSoftware.Lib.Requests.Builder;
using DaveCommonsSoftware.Lib.Requests.Data;
using Moq;
using Moq.Protected;
using Requests.Test.Mock.Mocks;
using Requests.Test.Mock.Samples.Login;
using System.Net;

namespace Requests.Test.InplaceUsage
{
    public class ContextBuildTest
    {
        [Fact]
        public async Task Requests_SendAsync_HttpMessageHandlerSendAsyncCalled()
        {
            // - Arrange ------
            var mockedMessageHandler = HttpClientMock.GetMockedHttpMessageHandler("", HttpStatusCode.OK);
            var mockedHttpClient = new HttpClient(mockedMessageHandler.Object);

            // - Act ----------
            RequestsResult<string> result = await RequestBuilder
                .Post<string>("https://example.com/auth", context => context.HttpRequestClient = mockedHttpClient)
                .SendAsync();

            // - Assert -------
            mockedMessageHandler
                .Protected()
                .Verify("SendAsync", Times.Once(), ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>());
        }

        [Fact]
        public async Task Requests_SendAsync_ResponseObjectStatusCodeFieldFilled()
        {
            // - Arrange ------
            var targetStatusCode = HttpStatusCode.OK;
            var mockedHttpClient = HttpClientMock.Create("", targetStatusCode);

            // - Act ----------
            RequestsResult<string> result = await RequestBuilder
                .Post<string>("https://example.com/auth", context => context.HttpRequestClient = mockedHttpClient)
                .SendAsync();

            // - Assert -------
            Assert.Equal(targetStatusCode, result.StatusCode);
        }

        [Fact]
        public async Task Requests_SendAsync_ResponseObjectUriFieldFilled()
        {
            // - Arrange ------
            var targetUri = "https://example.com/auth";
            var mockedHttpClient = HttpClientMock.Create("", HttpStatusCode.OK);

            // - Act ----------
            RequestsResult<string> result = await RequestBuilder
                .Post<string>(targetUri, context => context.HttpRequestClient = mockedHttpClient)
                .SendAsync();

            // - Assert -------
            Assert.Equal(targetUri, result.FinalUri);
        }

        [Fact]
        public async Task Requests_SendAsync_ResponseJsonBodySerialized()
        {
            // - Arrange ------
            var responseBody = new SampleLoginResponse()
            {
                Token = "02fdas.t0k3n",
                RefreshToken = "0daw41d4r3fr3sht0k3n=="
            };
            var mockedHttpClient = HttpClientMock.Create(responseBody, HttpStatusCode.OK);

            // - Act ----------
            RequestsResult<SampleLoginResponse> result = await RequestBuilder
                .Post<SampleLoginResponse>("https://example.com/auth", context => context.HttpRequestClient = mockedHttpClient)
                .SendAsync();

            // - Assert -------
            Assert.NotNull(result.Data);
            Assert.Equal(responseBody.Token, result.Data.Token);
            Assert.Equal(responseBody.RefreshToken, result.Data.RefreshToken);
        }
    }
}