using FluentRequests.Builder.Headers.Auth;
using FluentRequests.Data;
using FluentRequests.Exceptions;

namespace FluentRequests.Builder
{
    public class RequestBuilder<TResponse>(RequestContext context)
    {
        public RequestBuilder<TResponse> WithBody(object body)
        {
            context.Body = body;
            return this;
        }

        public RequestBuilder<TResponse> WithQuery(object query)
        {
            context.Query = query;
            return this;
        }

        public RequestBuilder<TResponse> WithRouteParams(object routeParams)
        {
            context.RouteParams = routeParams;
            return this;
        }

        public RequestBuilder<TResponse> WithAuth(IAuthHeader header)
        {
            context.Headers.Add("Authentication", header);
            return this;
        }

        public RequestBuilder<TResponse> WithAuth(string headerAuthPropertyName, IAuthHeader header)
        {
            context.Headers.Add(headerAuthPropertyName, header);
            return this;
        }

        public RequestBuilder<TResponse> WithMaxRetries(int maxRetries)
        {
            context.MaxRetries = maxRetries;
            return this;
        }

        public RequestBuilder<TResponse> WithPagination(int page, int pageSize)
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
