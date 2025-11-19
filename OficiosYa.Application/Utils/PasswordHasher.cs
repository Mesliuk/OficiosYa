using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OficiosYa.Application.Utils
{
    public static class PasswordHasher
    {
        public static string Hash(string password)
        {
            // TODO: implement real hashing
            return $"HASHED_{password}";
        }


        public static bool Verify(string password, string hashed)
        {
            // TODO: implement real verify
            return hashed == $"HASHED_{password}";
        }
    }


    public static class TokenGenerator
    {
        public static string GenerateToken(string input)
        {
            // TODO: implement real token generation
            return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(input));
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
