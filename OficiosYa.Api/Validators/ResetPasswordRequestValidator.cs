using FluentValidation;
using OficiosYa.Api.Models;

namespace OficiosYa.Api.Validators
{
    public class ResetPasswordRequestValidator : AbstractValidator<ResetPasswordRequest>
    {
        public ResetPasswordRequestValidator()
        {
            RuleFor(x => x.Correo).NotEmpty().EmailAddress().MaximumLength(150);
            RuleFor(x => x.Codigo).NotEmpty();
            RuleFor(x => x.NuevaPassword).NotEmpty().MinimumLength(8);
        }
    }
}
