using FluentRequests.Builder;
using FluentRequests.Data;
using System.Net;

namespace FluentRequests;

public static class Requests
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
    public static RequestBuilder<TResponse> Post<TResponse>(string uri) =>
        new(new RequestContext() { Uri = uri, HttpRequestClient = _httpClient, Method = HttpMethod.Post });

    public static RequestBuilder<TResponse> Post<TResponse>(string uri, RequestContext context) =>
        CreateBuilderWithProvidedContext<TResponse>(uri, HttpMethod.Post, context);

    public static RequestBuilder<TResponse> Post<TResponse>(string uri, Action<RequestContext> contextBuilder) =>
        CreateBuilderWithContextBuilderAction<TResponse>(uri, HttpMethod.Post, contextBuilder);

    // - GET -------------------------------------
    public static RequestBuilder<TResponse> Get<TResponse>(string uri) =>
        new(new RequestContext() { Uri = uri, HttpRequestClient = _httpClient, Method = HttpMethod.Get });

    public static RequestBuilder<TResponse> Get<TResponse>(string uri, RequestContext context) =>
        CreateBuilderWithProvidedContext<TResponse>(uri, HttpMethod.Get, context);

    public static RequestBuilder<TResponse> Get<TResponse>(string uri, Action<RequestContext> contextBuilder) =>
        CreateBuilderWithContextBuilderAction<TResponse>(uri, HttpMethod.Get, contextBuilder);

    // - PUT -------------------------------------
    public static RequestBuilder<TResponse> Put<TResponse>(string uri) =>
        new(new RequestContext() { Uri = uri, HttpRequestClient = _httpClient, Method = HttpMethod.Put });

    public static RequestBuilder<TResponse> Put<TResponse>(string uri, RequestContext context) =>
        CreateBuilderWithProvidedContext<TResponse>(uri, HttpMethod.Put, context);

    public static RequestBuilder<TResponse> Put<TResponse>(string uri, Action<RequestContext> contextBuilder) =>
        CreateBuilderWithContextBuilderAction<TResponse>(uri, HttpMethod.Put, contextBuilder);

    // - PATCH -----------------------------------
    public static RequestBuilder<TResponse> Patch<TResponse>(string uri) =>
        new(new RequestContext() { Uri = uri, HttpRequestClient = _httpClient, Method = HttpMethod.Patch });

    public static RequestBuilder<TResponse> Patch<TResponse>(string uri, RequestContext context) =>
        CreateBuilderWithProvidedContext<TResponse>(uri, HttpMethod.Patch, context);

    public static RequestBuilder<TResponse> Patch<TResponse>(string uri, Action<RequestContext> contextBuilder) =>
        CreateBuilderWithContextBuilderAction<TResponse>(uri, HttpMethod.Patch, contextBuilder);

    // - DELETE ----------------------------------
    public static RequestBuilder<TResponse> Delete<TResponse>(string uri) =>
        new(new RequestContext() { Uri = uri, HttpRequestClient = _httpClient, Method = HttpMethod.Delete });

    public static RequestBuilder<TResponse> Delete<TResponse>(string uri, RequestContext context) =>
        CreateBuilderWithProvidedContext<TResponse>(uri, HttpMethod.Delete, context);

    public static RequestBuilder<TResponse> Delete<TResponse>(string uri, Action<RequestContext> contextBuilder) =>
        CreateBuilderWithContextBuilderAction<TResponse>(uri, HttpMethod.Delete, contextBuilder);

    // - Utility ----------------------------------
    private static RequestBuilder<TResponse> CreateBuilderWithProvidedContext<TResponse>(string uri, HttpMethod method, RequestContext context)
    {
        context.Uri = uri;
        context.Method = method;

        return new RequestBuilder<TResponse>(context);
    }

    private static RequestBuilder<TResponse> CreateBuilderWithContextBuilderAction<TResponse>(string uri, HttpMethod method, Action<RequestContext> contextBuilder)
    {
        var context = new RequestContext(); 
        contextBuilder.Invoke(context);
        
        context.Method = method;
        context.Uri = uri;
        context.HttpRequestClient ??= _httpClient;

        return new RequestBuilder<TResponse>(context); 
    }
}
