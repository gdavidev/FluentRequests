using DaveCommonsSoftware.Lib.Requests.Abstractions.Builder.Headers;

namespace DaveCommonsSoftware.Lib.Requests.Abstractions.Data
{
    public interface IRequestContext
    {
        public HttpClient? HttpRequestClient { get; internal set; }

        public Dictionary<string, IHttpHeader> Headers { get; set; }
        public int CurrentTry { get; internal set; }
        public int? MaxRetries { get; set; }

        public object? Body { get; set; }
        public object? Query { get; set; }
        public object? RouteParams { get; set; }

        public HttpMethod Method { get; internal set; }
        public string Uri { get; internal set; }

        // Pagination
        public int CurrentPage { get; internal set; }
        public int PageSize { get; internal set; }
    }
}
