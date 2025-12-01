using FluentValidation;
using OficiosYa.Api.Models;
using OficiosYa.Application.Interfaces;

namespace OficiosYa.Api.Validators
{
    public class RegisterClienteRequestValidator : AbstractValidator<RegisterClienteRequest>
    {
        public RegisterClienteRequestValidator(IUsuarioRepository usuarioRepo)
        {
            RuleFor(x => x.Nombre).NameRule();
            RuleFor(x => x.Apellido).LastNameRule();
            RuleFor(x => x.Correo)
                .EmailFormatRule()
                .MustAsync(async (email, ct) =>
                {
                    var existing = await usuarioRepo.ObtenerPorEmailAsync(email);
                    return existing == null;
                }).WithMessage("El correo ya está registrado");

            RuleFor(x => x.Telefono).PhoneFormatRule();
            RuleFor(x => x.Password).PasswordStrengthRule();

            // Validaciones opcionales de dirección
            RuleFor(x => x.Direccion).MaximumLength(500).When(x => !string.IsNullOrEmpty(x.Direccion))
                .WithMessage("La dirección no puede exceder los 500 caracteres.");
            RuleFor(x => x.Descripcion).MaximumLength(100).When(x => !string.IsNullOrEmpty(x.Descripcion))
                .WithMessage("El alias no puede exceder los 100 caracteres.");
        }
    }
}
