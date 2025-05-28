namespace FluentRequests.Builder.Headers.Auth
{
    public interface IAuthHeader : IHttpHeader
    {
        public string Serialize();
    }
}
