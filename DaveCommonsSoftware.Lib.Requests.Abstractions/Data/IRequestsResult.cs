using System.Net;

namespace DaveCommonsSoftware.Lib.Requests.Abstractions.Data
{
    public interface IRequestsResult<TResponse>
    {
        IRequestContext Context { get; set; }
        TResponse? Data { get; }
        string FinalUri { get; }
        HttpStatusCode StatusCode { get; }
        int TimeMs { get; }

        void Retry();
    }
}
