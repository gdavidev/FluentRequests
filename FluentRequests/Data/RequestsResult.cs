using System.Net;

namespace FluentRequests.Data
{
    public class RequestsResult<TResponse>
    {
        public string Uri { get; internal set; } = string.Empty;
        public TResponse? Data { get; internal set; }
        public HttpStatusCode StatusCode { get; internal set; }
        public int TimeMs { get; internal set; }
        public RequestContext Context { get; set; }

        public void Retry()
        {
            throw new NotImplementedException();
        }
    }
}
