using System;
using System.Text;

namespace Infrastructure.Resources.Hashing
{
    class BCryptHashingProvider : IHashingService
    {
        private static readonly Random Random = new Random();
        private const string Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        public string GenerateSalt()
        {
            var data = new byte[64];
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = (byte)Random.Next(0, 128);
            }
            var encoding = new UTF8Encoding();
            return encoding.GetString(data);
        }

        public string HashPassword(string password, string salt)
        {
            return BCrypt.Net.BCrypt.HashPassword(InjectSalt(password, salt));
        }

        public bool CheckPassword(string hash, string password, string salt)
        {
            return BCrypt.Net.BCrypt.Verify(InjectSalt(password, salt), hash);
        }

        private string InjectSalt(string password, string salt)
        {
            return salt.Substring(0, 32) + password + salt.Substring(32);
        }
    }
}
