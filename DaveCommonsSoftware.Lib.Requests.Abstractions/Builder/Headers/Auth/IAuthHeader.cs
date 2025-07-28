namespace DaveCommonsSoftware.Lib.Requests.Abstractions.Builder.Headers.Auth
{
    public interface IAuthHeader : IHttpHeader
    {
        public string Serialize();
    }
}
