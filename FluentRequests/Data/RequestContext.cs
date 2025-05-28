using FluentRequests.Builder.Headers;

namespace FluentRequests.Data
{
    public class RequestContext
    {
        public required HttpMethod Method { get; set; } = HttpMethod.Get;
        public string Url { get; set; } = "";
        public Dictionary<string, IHttpHeader> Headers { get; set; } = [];
        public object? Body { get; set; } = null;
        public object? Query { get; set; } = null;
        public object? Form { get; set; } = null;
        public object? RouteParams { get; set; } = null;
        public int CurrentTry { get; set; } = 0;
        public int? MaxRetries { get; set; } = null;
    }
}
