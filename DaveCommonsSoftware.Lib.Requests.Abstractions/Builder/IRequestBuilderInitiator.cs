using DaveCommonsSoftware.Lib.Requests.Abstractions.Builder.Headers.Auth;
using DaveCommonsSoftware.Lib.Requests.Abstractions.Data;

namespace DaveCommonsSoftware.Lib.Requests.Abstractions.Builder
{
    public interface IRequestBuilderInitiator<TResponse>
    {
        IRequestsResult<TResponse> Build();
        Task<IRequestsResult<TResponse>> SendAsync();
        IRequestBuilderInitiator<TResponse> WithAuth(IAuthHeader header);
        IRequestBuilderInitiator<TResponse> WithAuth(string headerAuthPropertyName, IAuthHeader header);
        IRequestBuilderInitiator<TResponse> WithBody(object body);
        IRequestBuilderInitiator<TResponse> WithMaxRetries(int maxRetries);
        IRequestBuilderInitiator<TResponse> WithPagination(int page, int pageSize);
        IRequestBuilderInitiator<TResponse> WithQuery(object query);
        IRequestBuilderInitiator<TResponse> WithRouteParams(object routeParams);
    }
}
