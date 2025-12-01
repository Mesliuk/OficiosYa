namespace OficiosYa.Api.Validators.Helpers
{
    public static class PasswordHelper
    {
        public static bool HasStrongPassword(string password)
        {
            if (string.IsNullOrEmpty(password)) return false;
            bool hasUpper = false, hasLower = false, hasDigit = false, hasSpecial = false;
            foreach (var c in password)
            {
                if (char.IsUpper(c)) hasUpper = true;
                else if (char.IsLower(c)) hasLower = true;
                else if (char.IsDigit(c)) hasDigit = true;
                else hasSpecial = true;
            }
            return hasUpper && hasLower && hasDigit && hasSpecial;
        }
    }
}
