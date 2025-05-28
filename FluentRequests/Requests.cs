using FluentRequests.Builder;
using FluentRequests.Data;

namespace FluentRequests;

public class Requests
{
    // - POST ------------------------------------
    public static RequestBuilder<TResponse> Post<TResponse>(string url) => 
        new(url, HttpMethod.Post);
    
    public static RequestBuilder<TResponse> Post<TResponse>(string url, RequestContext context) =>
        CreateBuilderWithProvidedContext<TResponse>(url, HttpMethod.Post, context);

    public static RequestBuilder<TResponse> Post<TResponse>(string url, Action<RequestContext> contextBuilder) =>
        CreateBuilderWithContextBuilderAction<TResponse>(url, HttpMethod.Post, contextBuilder);

    // - GET -------------------------------------
    public static RequestBuilder<TResponse> Get<TResponse>(string url) =>
        new(url, HttpMethod.Get);

    public static RequestBuilder<TResponse> Get<TResponse>(string url, RequestContext context) =>
        CreateBuilderWithProvidedContext<TResponse>(url, HttpMethod.Get, context);

    public static RequestBuilder<TResponse> Get<TResponse>(string url, Action<RequestContext> contextBuilder) =>
        CreateBuilderWithContextBuilderAction<TResponse>(url, HttpMethod.Get, contextBuilder);

    // - PUT -------------------------------------
    public static RequestBuilder<TResponse> Put<TResponse>(string url) =>
        new(url, HttpMethod.Put);

    public static RequestBuilder<TResponse> Put<TResponse>(string url, RequestContext context) =>
        CreateBuilderWithProvidedContext<TResponse>(url, HttpMethod.Put, context);

    public static RequestBuilder<TResponse> Put<TResponse>(string url, Action<RequestContext> contextBuilder) =>
        CreateBuilderWithContextBuilderAction<TResponse>(url, HttpMethod.Put, contextBuilder);

    // - PATCH -----------------------------------
    public static RequestBuilder<TResponse> Patch<TResponse>(string url) =>
        new(url, HttpMethod.Patch);

    public static RequestBuilder<TResponse> Patch<TResponse>(string url, RequestContext context) =>
        CreateBuilderWithProvidedContext<TResponse>(url, HttpMethod.Patch, context);

    public static RequestBuilder<TResponse> Patch<TResponse>(string url, Action<RequestContext> contextBuilder) =>
        CreateBuilderWithContextBuilderAction<TResponse>(url, HttpMethod.Patch, contextBuilder);

    // - DELETE ----------------------------------
    public static RequestBuilder<TResponse> Delete<TResponse>(string url) =>
        new(url, HttpMethod.Delete);

    public static RequestBuilder<TResponse> Delete<TResponse>(string url, RequestContext context) =>
        CreateBuilderWithProvidedContext<TResponse>(url, HttpMethod.Delete, context);

    public static RequestBuilder<TResponse> Delete<TResponse>(string url, Action<RequestContext> contextBuilder) =>
        CreateBuilderWithContextBuilderAction<TResponse>(url, HttpMethod.Delete, contextBuilder);

    private static RequestBuilder<TResponse> CreateBuilderWithProvidedContext<TResponse>(string url, HttpMethod method, RequestContext context)
    {
        context.Method = method;

        return new RequestBuilder<TResponse>(url, method)
        {
            Context = context
        };
    }

    private static RequestBuilder<TResponse> CreateBuilderWithContextBuilderAction<TResponse>(string url, HttpMethod method, Action<RequestContext> contextBuilder)
    {
        var builder = new RequestBuilder<TResponse>(url, method);

        contextBuilder(builder.Context);
        return builder;
    }
}
