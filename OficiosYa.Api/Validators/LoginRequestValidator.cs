using FluentValidation;
using OficiosYa.Api.Models;

namespace OficiosYa.Api.Validators
{
    public class LoginRequestValidator : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidator()
        {
            RuleFor(x => x.Correo)
                .NotEmpty()
                .EmailAddress()
                .MaximumLength(150);

            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(6);

            RuleFor(x => x.Role)
                .Must(role => string.IsNullOrEmpty(role) || role == "Cliente" || role == "Profesional")
                .WithMessage("Role must be either 'Cliente' or 'Profesional' if provided");
        }
    }
}
