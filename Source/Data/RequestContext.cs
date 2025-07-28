using DaveCommonsSoftware.Lib.Requests.Abstractions.Builder.Headers;
using DaveCommonsSoftware.Lib.Requests.Abstractions.Data;

namespace DaveCommonsSoftware.Lib.Requests.Data
{   
    public class RequestContext : IRequestContext
    {
        public HttpClient? HttpRequestClient { get; set; } = null;

        public Dictionary<string, IHttpHeader> Headers { get; set; } = [];
        public int CurrentTry { get; set; } = 0;
        public int? MaxRetries { get; set; } = null;

        public object? Body { get; set; }
        public object? Query { get; set; }        
        public object? RouteParams { get; set; }

        public HttpMethod Method { get; set; } = HttpMethod.Get;
        public string Uri { get; set; } = "";

        // Pagination
        public int CurrentPage { get; set; } = 0;
        public int PageSize { get; set; } = 0;
    }
}
