using DaveCommonsSoftware.Lib.Requests.Abstractions.Data;
using DaveCommonsSoftware.Lib.Requests.Builder;
using DaveCommonsSoftware.Lib.Requests.Data;
using Requests.Test.Mock.Mocks;

namespace Requests.Test.InplaceUsage.DataBinding;

public class WithQueryTest
{
    [Fact]
    public async Task RequestBuilder_WithQueryDictionaryStringString_SerializeDictionaryIntoAQueryString()
    {
        // - Arrange ------
        var sampleUri = "http://example.com";
        var queryParamsDict = new Dictionary<string, string>()
        {
          { "page", "2" },
          { "pageSize", "10" }
        };
        var mockedHttpClient = HttpClientMock.Create(expectedResponseBody: "");

        // - Act ----------
        IRequestsResult<string> result = await RequestBuilder
            .Post<string>(sampleUri, context => context.HttpRequestClient = mockedHttpClient)
            .WithQuery(queryParamsDict)
            .SendAsync();

        // - Assert -------
        Assert.Equal($"{sampleUri}?page=2&pageSize=10", result.FinalUri);
    }

    [Fact]
    public async Task RequestBuilder_WithQueryDictionaryStringObject_SerializeDictionaryIntoAQueryString()
    {
        // - Arrange ------
        string sampleUri = "http://example.com";
        var queryParamsDict = new Dictionary<string, object>()
        {
          { "page", 2 },
          { "pageSize", 10 },
          { "term", "Produto" }
        };
        var mockedHttpClient = HttpClientMock.Create(expectedResponseBody: "");

        // - Act ----------
        IRequestsResult<string> result = await RequestBuilder
            .Post<string>(sampleUri, context => context.HttpRequestClient = mockedHttpClient)
            .WithQuery(queryParamsDict)
            .SendAsync();

        // - Assert -------
        Assert.Equal($"{sampleUri}?page=2&pageSize=10&term=Produto", result.FinalUri);
    }

    [Fact]
    public async Task RequestBuilder_WithQueryAnonymousObject_SerializeObjectIntoAQueryString()
    {
        // - Arrange ------
        string sampleUri = "http://example.com";
        object queryParamsObject = new
        {
            Page = 2,
            PageSize = 10
        };
        var mockedHttpClient = HttpClientMock.Create(expectedResponseBody: "");

        // - Act ----------
        IRequestsResult<string> result = await RequestBuilder
            .Post<string>(sampleUri, context => context.HttpRequestClient = mockedHttpClient)
            .WithQuery(queryParamsObject)
            .SendAsync();

        // - Assert -------
        Assert.Equal($"{sampleUri}?page=2&pageSize=10", result.FinalUri);
    }
}
