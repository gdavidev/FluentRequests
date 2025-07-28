using DaveCommonsSoftware.Lib.Requests.Abstractions.Data;

namespace DaveCommonsSoftware.Lib.Requests.Abstractions.Builder;

public interface IRequestBuilder
{
    static abstract IRequestBuilderInitiator<TResponse> Delete<TResponse>(string uri);
    static abstract IRequestBuilderInitiator<TResponse> Delete<TResponse>(string uri, Action<IRequestContext> contextBuilder);
    static abstract IRequestBuilderInitiator<TResponse> Delete<TResponse>(string uri, IRequestContext context);
    static abstract IRequestBuilderInitiator<TResponse> Get<TResponse>(string uri);
    static abstract IRequestBuilderInitiator<TResponse> Get<TResponse>(string uri, Action<IRequestContext> contextBuilder);
    static abstract IRequestBuilderInitiator<TResponse> Get<TResponse>(string uri, IRequestContext context);
    static abstract IRequestBuilderInitiator<TResponse> Patch<TResponse>(string uri);
    static abstract IRequestBuilderInitiator<TResponse> Patch<TResponse>(string uri, Action<IRequestContext> contextBuilder);
    static abstract IRequestBuilderInitiator<TResponse> Patch<TResponse>(string uri, IRequestContext context);
    static abstract IRequestBuilderInitiator<TResponse> Post<TResponse>(string uri);
    static abstract IRequestBuilderInitiator<TResponse> Post<TResponse>(string uri, Action<IRequestContext> contextBuilder);
    static abstract IRequestBuilderInitiator<TResponse> Post<TResponse>(string uri, IRequestContext context);
    static abstract IRequestBuilderInitiator<TResponse> Put<TResponse>(string uri);
    static abstract IRequestBuilderInitiator<TResponse> Put<TResponse>(string uri, Action<IRequestContext> contextBuilder);
    static abstract IRequestBuilderInitiator<TResponse> Put<TResponse>(string uri, IRequestContext context);
}
