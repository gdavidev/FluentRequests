using DaveCommonsSoftware.Lib.Requests.Abstractions.Builder;
using DaveCommonsSoftware.Lib.Requests.Abstractions.Data;
using DaveCommonsSoftware.Lib.Requests.Data;
using System.Net;

namespace DaveCommonsSoftware.Lib.Requests.Builder;

public class RequestBuilder : IRequestBuilder
{
    private static readonly HttpClient _httpClient = CreateDefaultClient();

    private static HttpClient CreateDefaultClient()
    {
        var handler = new HttpClientHandler
        {
            AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
        };
        return new HttpClient(handler)
        {
            Timeout = TimeSpan.FromSeconds(30)
        };
    }

    // - POST ------------------------------------
    public static IRequestBuilderInitiator<TResponse> Post<TResponse>(string uri) =>
        new RequestBuilderInitiator<TResponse>(new RequestContext() { Uri = uri, HttpRequestClient = _httpClient, Method = HttpMethod.Post });

    public static IRequestBuilderInitiator<TResponse> Post<TResponse>(string uri, IRequestContext context) =>
        CreateBuilderWithProvidedContext<TResponse>(uri, HttpMethod.Post, context);

    public static IRequestBuilderInitiator<TResponse> Post<TResponse>(string uri, Action<IRequestContext> contextBuilder) =>
        CreateBuilderWithContextBuilderAction<TResponse>(uri, HttpMethod.Post, contextBuilder);

    // - GET -------------------------------------
    public static IRequestBuilderInitiator<TResponse> Get<TResponse>(string uri) =>
        new RequestBuilderInitiator<TResponse>(new RequestContext() { Uri = uri, HttpRequestClient = _httpClient, Method = HttpMethod.Get });

    public static IRequestBuilderInitiator<TResponse> Get<TResponse>(string uri, IRequestContext context) =>
        CreateBuilderWithProvidedContext<TResponse>(uri, HttpMethod.Get, context);

    public static IRequestBuilderInitiator<TResponse> Get<TResponse>(string uri, Action<IRequestContext> contextBuilder) =>
        CreateBuilderWithContextBuilderAction<TResponse>(uri, HttpMethod.Get, contextBuilder);

    // - PUT -------------------------------------
    public static IRequestBuilderInitiator<TResponse> Put<TResponse>(string uri) =>
        new RequestBuilderInitiator<TResponse>(new RequestContext() { Uri = uri, HttpRequestClient = _httpClient, Method = HttpMethod.Put });

    public static IRequestBuilderInitiator<TResponse> Put<TResponse>(string uri, IRequestContext context) =>
        CreateBuilderWithProvidedContext<TResponse>(uri, HttpMethod.Put, context);

    public static IRequestBuilderInitiator<TResponse> Put<TResponse>(string uri, Action<IRequestContext> contextBuilder) =>
        CreateBuilderWithContextBuilderAction<TResponse>(uri, HttpMethod.Put, contextBuilder);

    // - PATCH -----------------------------------
    public static IRequestBuilderInitiator<TResponse> Patch<TResponse>(string uri) =>
        new RequestBuilderInitiator<TResponse>(new RequestContext() { Uri = uri, HttpRequestClient = _httpClient, Method = HttpMethod.Patch });

    public static IRequestBuilderInitiator<TResponse> Patch<TResponse>(string uri, IRequestContext context) =>
        CreateBuilderWithProvidedContext<TResponse>(uri, HttpMethod.Patch, context);

    public static IRequestBuilderInitiator<TResponse> Patch<TResponse>(string uri, Action<IRequestContext> contextBuilder) =>
        CreateBuilderWithContextBuilderAction<TResponse>(uri, HttpMethod.Patch, contextBuilder);

    // - DELETE ----------------------------------
    public static IRequestBuilderInitiator<TResponse> Delete<TResponse>(string uri) =>
        new RequestBuilderInitiator<TResponse>(new RequestContext() { Uri = uri, HttpRequestClient = _httpClient, Method = HttpMethod.Delete });

    public static IRequestBuilderInitiator<TResponse> Delete<TResponse>(string uri, IRequestContext context) =>
        CreateBuilderWithProvidedContext<TResponse>(uri, HttpMethod.Delete, context);

    public static IRequestBuilderInitiator<TResponse> Delete<TResponse>(string uri, Action<IRequestContext> contextBuilder) =>
        CreateBuilderWithContextBuilderAction<TResponse>(uri, HttpMethod.Delete, contextBuilder);

    // - Utility ----------------------------------
    private static IRequestBuilderInitiator<TResponse> CreateBuilderWithProvidedContext<TResponse>(string uri, HttpMethod method, IRequestContext context)
    {
        context.Uri = uri;
        context.Method = method;

        return new RequestBuilderInitiator<TResponse>(context);
    }

    private static RequestBuilderInitiator<TResponse> CreateBuilderWithContextBuilderAction<TResponse>(string uri, HttpMethod method, Action<IRequestContext> contextBuilder)
    {
        var context = new RequestContext();
        contextBuilder.Invoke(context);

        context.Method = method;
        context.Uri = uri;
        context.HttpRequestClient ??= _httpClient;

        return new RequestBuilderInitiator<TResponse>(context);
    }
}
