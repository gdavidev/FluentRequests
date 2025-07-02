using FluentRequests.Builder.Headers;

namespace FluentRequests.Data
{
    public class RequestContext
    {
        internal HttpClient? HttpRequestClient { get; set; } = null;

        public HttpMethod Method { get; internal set; } = HttpMethod.Get;
        public string Uri { get; internal set; } = "";
        public Dictionary<string, IHttpHeader> Headers { get; set; } = [];
        public object? Body { get; set; }
        public object? Query { get; set; }
        public object? Form { get; set; }
        public object? RouteParams { get; set; }
        public int CurrentTry { get; internal set; } = 0;
        public int? MaxRetries { get; set; } = null;

        // Pagination
        public int CurrentPage { get; internal set; } = 0;
        public int PageSize { get; internal set; } = 0;
    }
}
