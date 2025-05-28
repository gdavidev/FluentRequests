using FluentRequests.Builder.Headers.Auth;
using FluentRequests.Data;

namespace FluentRequests.Builder
{
    public class RequestBuilder<TResponse>
    {
        public RequestContext Context { get; set; }

        public RequestBuilder(string url, RequestContext context)
        {
            Context = context;
            Context.Url = url;
        }

        public RequestBuilder(string url, HttpMethod? method)
        {            
            Context = new RequestContext() { Method = method ?? HttpMethod.Get };
            Context.Url = url;
        }

        public RequestBuilder<TResponse> WithBody(object body)
        {
            Context.Body = body;
            return this;
        }

        public RequestBuilder<TResponse> WithQuery<TQuery>(TQuery query)
        {
            Context.Query = query;
            return this;
        }

        public RequestBuilder<TResponse> WithAuth(IAuthHeader header)
        {
            Context.Headers.Add("Authentication", header);
            return this;
        }

        public RequestBuilder<TResponse> WithAuth(string headerAuthPropertyName, IAuthHeader header)
        {
            Context.Headers.Add(headerAuthPropertyName, header);
            return this;
        }

        public RequestBuilder<TResponse> WithConfig(Action<RequestContext> contextBuilder)
        {
            contextBuilder(Context);
            return this;
        }

        public async Task<RequestsResult<TResponse>> SendAsync()
        {
            return new RequestsResult<TResponse>();
        }
    }
}
