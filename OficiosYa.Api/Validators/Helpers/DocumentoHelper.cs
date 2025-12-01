using System.Linq;

namespace OficiosYa.Api.Validators.Helpers
{
    public static class DocumentoHelper
    {
        // Valid for Argentina: only DNI with exactly 8 digits
        public static bool IsValidArgentinianDocument(string documento)
        {
            if (string.IsNullOrWhiteSpace(documento)) return false;

            // Remove non-digits
            var digits = new string(documento.Where(char.IsDigit).ToArray());
            // Only accept DNI with exactly 8 digits
            if (digits.Length == 8)
            {
                return true;
            }

            return false;
        }

        public static string NormalizeToDigits(string documento)
        {
            if (documento == null) return string.Empty;
            return new string(documento.Where(char.IsDigit).ToArray());
        }
    }
}
