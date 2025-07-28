namespace DaveCommonsSoftware.Lib.Requests.Abstractions.Data
{
    public interface IRequestsHttpClient
    {
        Task<IRequestsResult<TResponse>> SendRequestAsync<TResponse>(IRequestContext context);
    }
}
