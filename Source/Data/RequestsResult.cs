using DaveCommonsSoftware.Lib.Requests.Abstractions.Data;
using System.Net;

namespace DaveCommonsSoftware.Lib.Requests.Data
{
    public class RequestsResult<TResponse> : IRequestsResult<TResponse>
    {
        public string FinalUri { get; internal set; } = string.Empty;
        public TResponse? Data { get; internal set; }
        public HttpStatusCode StatusCode { get; internal set; }
        public int TimeMs { get; internal set; }
        public IRequestContext Context { get; set; }

        public void Retry()
        {
            throw new NotImplementedException();
        }
    }
}
