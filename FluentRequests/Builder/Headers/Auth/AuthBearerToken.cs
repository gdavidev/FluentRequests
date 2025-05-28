namespace FluentRequests.Builder.Headers.Auth
{
    public class AuthBearerToken(string token, string? refreshToken) : IAuthHeader
    {
        public string Token { get; } = token;
        public string? RefreshToken { get; } = refreshToken;

        public string Serialize()
        {
            return $"Bearer {Token}";
        }
    }
}
