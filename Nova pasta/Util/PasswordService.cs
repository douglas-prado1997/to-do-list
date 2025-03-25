using System.Security.Cryptography;
using System.Text;

namespace todo
{
    public class PasswordService
    {
        private readonly string _secretKey;

        public PasswordService(IConfiguration configuration)
        {
            _secretKey = configuration["PasswordSettings:SecretKey"];
        }

        public string GeneratePasswordHash(string password)
        {
            using (var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(_secretKey)))
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                byte[] hashBytes = hmac.ComputeHash(passwordBytes);
                return Convert.ToBase64String(hashBytes);
            }
        }

        public string VerifyPassword(string password)
        {
            return  GeneratePasswordHash(password);

        }
    }
}
