using DaveCommonsSoftware.Lib.Requests.Data;
using System.Net;

namespace DaveCommonsSoftware.Lib.Requests.Builder;

public static class RequestBuilder
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
    public static RequestBuilderInitiator<TResponse> Post<TResponse>(string uri) =>
        new(new RequestContext() { Uri = uri, HttpRequestClient = _httpClient, Method = HttpMethod.Post });

    public static RequestBuilderInitiator<TResponse> Post<TResponse>(string uri, RequestContext context) =>
        CreateBuilderWithProvidedContext<TResponse>(uri, HttpMethod.Post, context);

    public static RequestBuilderInitiator<TResponse> Post<TResponse>(string uri, Action<RequestContext> contextBuilder) =>
        CreateBuilderWithContextBuilderAction<TResponse>(uri, HttpMethod.Post, contextBuilder);

    // - GET -------------------------------------
    public static RequestBuilderInitiator<TResponse> Get<TResponse>(string uri) =>
        new(new RequestContext() { Uri = uri, HttpRequestClient = _httpClient, Method = HttpMethod.Get });

    public static RequestBuilderInitiator<TResponse> Get<TResponse>(string uri, RequestContext context) =>
        CreateBuilderWithProvidedContext<TResponse>(uri, HttpMethod.Get, context);

    public static RequestBuilderInitiator<TResponse> Get<TResponse>(string uri, Action<RequestContext> contextBuilder) =>
        CreateBuilderWithContextBuilderAction<TResponse>(uri, HttpMethod.Get, contextBuilder);

    // - PUT -------------------------------------
    public static RequestBuilderInitiator<TResponse> Put<TResponse>(string uri) =>
        new(new RequestContext() { Uri = uri, HttpRequestClient = _httpClient, Method = HttpMethod.Put });

    public static RequestBuilderInitiator<TResponse> Put<TResponse>(string uri, RequestContext context) =>
        CreateBuilderWithProvidedContext<TResponse>(uri, HttpMethod.Put, context);

    public static RequestBuilderInitiator<TResponse> Put<TResponse>(string uri, Action<RequestContext> contextBuilder) =>
        CreateBuilderWithContextBuilderAction<TResponse>(uri, HttpMethod.Put, contextBuilder);

    // - PATCH -----------------------------------
    public static RequestBuilderInitiator<TResponse> Patch<TResponse>(string uri) =>
        new(new RequestContext() { Uri = uri, HttpRequestClient = _httpClient, Method = HttpMethod.Patch });

    public static RequestBuilderInitiator<TResponse> Patch<TResponse>(string uri, RequestContext context) =>
        CreateBuilderWithProvidedContext<TResponse>(uri, HttpMethod.Patch, context);

    public static RequestBuilderInitiator<TResponse> Patch<TResponse>(string uri, Action<RequestContext> contextBuilder) =>
        CreateBuilderWithContextBuilderAction<TResponse>(uri, HttpMethod.Patch, contextBuilder);

    // - DELETE ----------------------------------
    public static RequestBuilderInitiator<TResponse> Delete<TResponse>(string uri) =>
        new(new RequestContext() { Uri = uri, HttpRequestClient = _httpClient, Method = HttpMethod.Delete });

    public static RequestBuilderInitiator<TResponse> Delete<TResponse>(string uri, RequestContext context) =>
        CreateBuilderWithProvidedContext<TResponse>(uri, HttpMethod.Delete, context);

    public static RequestBuilderInitiator<TResponse> Delete<TResponse>(string uri, Action<RequestContext> contextBuilder) =>
        CreateBuilderWithContextBuilderAction<TResponse>(uri, HttpMethod.Delete, contextBuilder);

    // - Utility ----------------------------------
    private static RequestBuilderInitiator<TResponse> CreateBuilderWithProvidedContext<TResponse>(string uri, HttpMethod method, RequestContext context)
    {
        context.Uri = uri;
        context.Method = method;

        return new RequestBuilderInitiator<TResponse>(context);
    }

    private static RequestBuilderInitiator<TResponse> CreateBuilderWithContextBuilderAction<TResponse>(string uri, HttpMethod method, Action<RequestContext> contextBuilder)
    {
        var context = new RequestContext(); 
        contextBuilder.Invoke(context);
        
        context.Method = method;
        context.Uri = uri;
        context.HttpRequestClient ??= _httpClient;

        return new RequestBuilderInitiator<TResponse>(context); 
    }
}
