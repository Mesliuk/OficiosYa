using System;
using System.Security.Cryptography;
using System.Text;

namespace OficiosYa.Application.Utils
{
    public static class PasswordHasher
    {
        // Format: v1$iterations$saltBase64$hashBase64
        private const int Iterations = 100_000;
        private const int SaltSize = 16; // 128 bits
        private const int HashSize = 32; // 256 bits

        public static string Hash(string password)
        {
            if (password == null) throw new ArgumentNullException(nameof(password));

            using var rng = RandomNumberGenerator.Create();
            var salt = new byte[SaltSize];
            rng.GetBytes(salt);

            using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA256);
            var hash = pbkdf2.GetBytes(HashSize);

            var result = $"v1${Iterations}${Convert.ToBase64String(salt)}${Convert.ToBase64String(hash)}";
            return result;
        }

        public static bool Verify(string password, string hashed)
        {
            if (password == null) throw new ArgumentNullException(nameof(password));
            if (hashed == null) return false;

            try
            {
                var parts = hashed.Split('$', StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length != 4) return false;
                if (parts[0] != "v1") return false;

                var iterations = int.Parse(parts[1]);
                var salt = Convert.FromBase64String(parts[2]);
                var hash = Convert.FromBase64String(parts[3]);

                using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations, HashAlgorithmName.SHA256);
                var computed = pbkdf2.GetBytes(hash.Length);

                return CryptographicOperations.FixedTimeEquals(computed, hash);
            }
            catch
            {
                return false;
            }
        }
    }

    public static class TokenGenerator
    {
        public static string GenerateToken(string input)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(input));
        }
    }

    public static class DateUtils
    {
        public static DateTime NowUtc()
        {
            return DateTime.UtcNow;
        }
    }
}
