using DevOne.Security.Cryptography.BCrypt;

namespace comercializadora_de_pulpo_api.Utilities
{
    public class Password
    {
        private readonly IConfiguration _configuration;

        public Password(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Encrypt(string password)
        {
            string salt = BCryptHelper.GenerateSalt();
            string passwordhashed = BCryptHelper.HashPassword(password, salt);

            return passwordhashed;
        }

        public bool Verify(string password, string passwordStored)
        {
            bool isEqual = BCryptHelper.CheckPassword(password, passwordStored);

            return isEqual;
        }

        //Hacer Verificación con Regex
    }
}
