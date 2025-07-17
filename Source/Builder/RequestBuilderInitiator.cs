using DaveCommonsSoftware.Lib.Requests.Builder.Headers.Auth;
using DaveCommonsSoftware.Lib.Requests.Data;
using DaveCommonsSoftware.Lib.Requests.Exceptions;

namespace DaveCommonsSoftware.Lib.Requests.Builder
{
    public class RequestBuilderInitiator<TResponse>(RequestContext context)
    {
        public RequestBuilderInitiator<TResponse> WithBody(object body)
        {
            context.Body = body;
            return this;
        }

        public RequestBuilderInitiator<TResponse> WithQuery(object query)
        {
            context.Query = query;
            return this;
        }

        public RequestBuilderInitiator<TResponse> WithRouteParams(object routeParams)
        {
            context.RouteParams = routeParams;
            return this;
        }

        public RequestBuilderInitiator<TResponse> WithAuth(IAuthHeader header)
        {
            context.Headers.Add("Authentication", header);
            return this;
        }

        public RequestBuilderInitiator<TResponse> WithAuth(string headerAuthPropertyName, IAuthHeader header)
        {
            context.Headers.Add(headerAuthPropertyName, header);
            return this;
        }

        public RequestBuilderInitiator<TResponse> WithMaxRetries(int maxRetries)
        {
            context.MaxRetries = maxRetries;
            return this;
        }

        public RequestBuilderInitiator<TResponse> WithPagination(int page, int pageSize)
        {
            context.CurrentPage = page;
            context.PageSize = pageSize;
            return this;
        }

        public RequestsResult<TResponse> Build()
        {
            if (context.HttpRequestClient is null)
                throw new RequestBuildException("Could not get HttpClient instance", "Build");
            
            return new RequestsResult<TResponse>();
        }

        public async Task<RequestsResult<TResponse>> SendAsync()
        {
            if (context.HttpRequestClient is null)
                throw new RequestBuildException("Could not get HttpClient instance", "SendAsync");

            var client = new RequestsHttpClient();
            var result = await client.SendRequestAsync<TResponse>(context);

            return result;
        }
    }
}
