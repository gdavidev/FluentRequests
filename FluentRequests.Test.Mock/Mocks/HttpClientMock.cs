﻿using Moq.Protected;
using Moq;
using System.Net;
using System.Text.Json;
using System.Text;

namespace FluentRequests.Test.Mock.Mocks
{
    public class HttpClientMock
    {
        public static HttpClient Create(
            object expectedResponseBody,
            HttpStatusCode expectedStatusCode = HttpStatusCode.OK)
        {
            var mockedMessageHandler = GetMockedHttpMessageHandler(expectedResponseBody, expectedStatusCode);
            return new HttpClient(mockedMessageHandler.Object);
        }

        public static Mock<HttpMessageHandler> GetMockedHttpMessageHandler(
            object expectedResponseBody,
            HttpStatusCode expectedStatusCode = HttpStatusCode.OK,
            Action<HttpRequestMessage, CancellationToken>? callback = null)
        {
            var json = JsonSerializer.Serialize(expectedResponseBody);

            var handlerMock = new Mock<HttpMessageHandler>();
            handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .Callback(callback)
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = expectedStatusCode,
                    Content = new StringContent(json, Encoding.UTF8, "application/json"),
                });

            return handlerMock;
        }
    }
}
