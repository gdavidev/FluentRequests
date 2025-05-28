namespace FluentRequests.Builder.Headers.Auth
{
    public class AuthBasic(string login, string password) : IAuthHeader
    {
        public string Login { get; set; } = login;
        public string Password { get; set; } = password;

        public string Serialize()
        {
            return $"Basic {Login} : {Password}";
        }
    }
}
