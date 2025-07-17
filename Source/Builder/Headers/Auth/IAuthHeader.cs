namespace DaveCommonsSoftware.Lib.Requests.Builder.Headers.Auth
{
    public interface IAuthHeader : IHttpHeader
    {
        public string Serialize();
    }
}
