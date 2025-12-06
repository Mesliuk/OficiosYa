using FluentValidation;
using System.Text.RegularExpressions;
using OficiosYa.Api.Validators.Helpers;

namespace OficiosYa.Api.Validators
{
    public static class ValidationExtensions
    {
        public static IRuleBuilderOptions<T, string> NameRule<T>(this IRuleBuilder<T, string> rule)
        {
            return rule.NotEmpty().MaximumLength(100);
        }

        public static IRuleBuilderOptions<T, string> LastNameRule<T>(this IRuleBuilder<T, string> rule)
        {
            return rule.NotEmpty().MaximumLength(100);
        }

        public static IRuleBuilderOptions<T, string> EmailFormatRule<T>(this IRuleBuilder<T, string> rule)
        {
            return rule.NotEmpty().EmailAddress().MaximumLength(150);
        }

        public static IRuleBuilderOptions<T, string> PhoneFormatRule<T>(this IRuleBuilder<T, string> rule)
        {
            return rule.NotEmpty().MaximumLength(30).Matches(new Regex("^\\+?[0-9]{7,15}$")).WithMessage("Telefono debe ser formato internacional válido (solo dígitos y opcional +)");
        }

        public static IRuleBuilderOptions<T, string> PasswordStrengthRule<T>(this IRuleBuilder<T, string> rule)
        {
            return rule.NotEmpty().MinimumLength(8).Must(PasswordHelper.HasStrongPassword).WithMessage("La contraseña debe incluir mayúsculas, minúsculas, dígitos y un caracter especial");
        }
    }
}
