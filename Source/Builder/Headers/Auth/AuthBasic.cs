using DaveCommonsSoftware.Lib.Requests.Abstractions.Builder.Headers.Auth;

namespace DaveCommonsSoftware.Lib.Requests.Builder.Headers.Auth
{
    public class AuthBasic(string login, string password) : IAuthHeader
    {
        public string Login { get; set; } = login;
        public string Password { get; set; } = password;

        public string Serialize() => $"Basic {Login}:{Password}";
    }
}
