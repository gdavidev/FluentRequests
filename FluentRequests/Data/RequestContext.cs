using FluentRequests.Builder.Headers;

namespace FluentRequests.Data
{
    public class RequestContext
    {
        public required HttpClient HttpRequestClient { get; set; }
        public required HttpMethod Method { get; set; } = HttpMethod.Get;
        public string Url { get; set; } = "";
        public Dictionary<string, IHttpHeader> Headers { get; set; } = [];
        public object? Body { get; set; }
        public object? Query { get; set; }
        public object? Form { get; set; }
        public object? RouteParams { get; set; }
        public int CurrentTry { get; set; } = 0;
        public int? MaxRetries { get; set; } = null;
    }
}
