using DaveCommonsSoftware.Lib.Requests.Abstractions.Data;
using DaveCommonsSoftware.Lib.Requests.Builder;
using Requests.Test.Mock.Mocks;
using Requests.Test.Mock.Samples.Login;
using System.Net;
using System.Text;
using System.Text.Json;

namespace Requests.Test.InplaceUsage.DataBinding
{
    public class WithBodyTest
    {
        [Fact]
        public async Task Request_WithBodyAsObject_BodySentAndSetOnContext()
        {
            // - Arrange ------
            var requestBody = new SampleLoginRequest()
            {
                Email = "lucario@gmail.com",
                Password = "pokemon123"
            };

            HttpRequestMessage? requestSent = null;
            var mockedMessageHandler = HttpClientMock.GetMockedHttpMessageHandler(
                "",
                HttpStatusCode.OK,
                (req, _) => requestSent = req);
            var mockedHttpClient = new HttpClient(mockedMessageHandler.Object);

            // - Act ----------
            IRequestsResult<string> result = await RequestBuilder
                .Post<string>("https://example.com/auth", context => context.HttpRequestClient = mockedHttpClient)
                .WithBody(requestBody)
                .SendAsync();

            // - Assert -------
            Assert.Equal(requestBody, result.Context.Body);
            Assert.NotNull(requestSent);

            var content = requestSent.Content;
            Assert.NotNull(content);

            string actualJson = await content.ReadAsStringAsync();
            string expectedJson = JsonSerializer.Serialize(requestBody);
            Assert.Equal(expectedJson, actualJson);
        }

        [Fact]
        public async Task Request_WithBodyAsPrimitive_BodySentAndSetOnContext()
        {
            // - Arrange ------
            var requestBody = "SampleText";
            var encodedRequestBody = new StringContent(requestBody, Encoding.UTF8, "application/json");
            var requestSent = (HttpRequestMessage?)null;

            var mockedMessageHandler = HttpClientMock.GetMockedHttpMessageHandler(
                "",
                HttpStatusCode.OK,
                (req, _) => requestSent = req);
            var mockedHttpClient = new HttpClient(mockedMessageHandler.Object);

            // - Act ----------
            IRequestsResult<string> result = await RequestBuilder
                .Post<string>("https://example.com/auth", context => context.HttpRequestClient = mockedHttpClient)
                .WithBody(requestBody)
                .SendAsync();

            // - Assert -------
            Assert.Equal(requestBody, result.Context.Body);
            Assert.NotNull(requestSent);

            var content = requestSent.Content;
            Assert.NotNull(content);

            string actualJson = await content.ReadAsStringAsync();
            string expectedJson = JsonSerializer.Serialize(requestBody);
            Assert.Equal(expectedJson, actualJson);
        }
    }
}
