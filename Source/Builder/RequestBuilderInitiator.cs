using DaveCommonsSoftware.Lib.Requests.Abstractions.Builder;
using DaveCommonsSoftware.Lib.Requests.Abstractions.Builder.Headers.Auth;
using DaveCommonsSoftware.Lib.Requests.Abstractions.Data;
using DaveCommonsSoftware.Lib.Requests.Data;
using DaveCommonsSoftware.Lib.Requests.Exceptions;

namespace DaveCommonsSoftware.Lib.Requests.Builder
{
    public class RequestBuilderInitiator<TResponse>(IRequestContext context) : IRequestBuilderInitiator<TResponse>
    {
        public IRequestBuilderInitiator<TResponse> WithBody(object body)
        {
            context.Body = body;
            return this;
        }

        public IRequestBuilderInitiator<TResponse> WithQuery(object query)
        {
            context.Query = query;
            return this;
        }

        public IRequestBuilderInitiator<TResponse> WithRouteParams(object routeParams)
        {
            context.RouteParams = routeParams;
            return this;
        }

        public IRequestBuilderInitiator<TResponse> WithAuth(IAuthHeader header)
        {
            context.Headers.Add("Authentication", header);
            return this;
        }

        public IRequestBuilderInitiator<TResponse> WithAuth(string headerAuthPropertyName, IAuthHeader header)
        {
            context.Headers.Add(headerAuthPropertyName, header);
            return this;
        }

        public IRequestBuilderInitiator<TResponse> WithMaxRetries(int maxRetries)
        {
            context.MaxRetries = maxRetries;
            return this;
        }

        public IRequestBuilderInitiator<TResponse> WithPagination(int page, int pageSize)
        {
            context.CurrentPage = page;
            context.PageSize = pageSize;
            return this;
        }

        public IRequestsResult<TResponse> Build()
        {
            if (context.HttpRequestClient is null)
                throw new RequestBuildException("Could not get HttpClient instance", "Build");

            return new RequestsResult<TResponse>();
        }

        public async Task<IRequestsResult<TResponse>> SendAsync()
        {
            if (context.HttpRequestClient is null)
                throw new RequestBuildException("Could not get HttpClient instance", "SendAsync");

            var client = new RequestsHttpClient();
            var result = await client.SendRequestAsync<TResponse>(context);

            return result;
        }
    }
}
