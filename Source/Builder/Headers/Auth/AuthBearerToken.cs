using DaveCommonsSoftware.Lib.Requests.Abstractions.Builder.Headers.Auth;

namespace DaveCommonsSoftware.Lib.Requests.Builder.Headers.Auth
{
    public class AuthBearerToken(string token, string? refreshToken) : IAuthHeader
    {
        public string Token { get; } = token;
        public string? RefreshToken { get; } = refreshToken;

        public string Serialize() => $"Bearer {Token}";
    }
}
