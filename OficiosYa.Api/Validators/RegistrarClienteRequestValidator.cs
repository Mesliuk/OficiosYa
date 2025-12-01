using FluentValidation;
using OficiosYa.Api.Models;
using OficiosYa.Api.Validators.Helpers;

namespace OficiosYa.Api.Validators
{
    public class RegistrarClienteRequestValidator : AbstractValidator<RegistrarClienteRequest>
    {
        public RegistrarClienteRequestValidator()
        {
            RuleFor(x => x.Nombre)
                .NotEmpty().WithMessage("El nombre es requerido.")
                .MaximumLength(100).WithMessage("El nombre no debe exceder 100 caracteres.");

            RuleFor(x => x.Apellido)
                .NotEmpty().WithMessage("El apellido es requerido.")
                .MaximumLength(100).WithMessage("El apellido no debe exceder 100 caracteres.");

            RuleFor(x => x.Correo)
                .NotEmpty().WithMessage("El correo es requerido.")
                .EmailAddress().WithMessage("Formato de correo inválido.")
                .MaximumLength(200);

            RuleFor(x => x.Telefono)
                .NotEmpty().WithMessage("El teléfono es requerido.")
                .MaximumLength(30).WithMessage("El teléfono no debe exceder 30 caracteres.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("La contraseña es requerida.")
                .MinimumLength(8).WithMessage("La contraseña debe tener al menos 8 caracteres.")
                .Must(PasswordHelper.HasStrongPassword)
                .WithMessage("La contraseña debe contener mayúscula, minúscula, dígito y carácter especial.");

            RuleFor(x => x.FotoPerfil)
                .MaximumLength(500).When(x => !string.IsNullOrEmpty(x.FotoPerfil))
                .WithMessage("La ruta de la foto no debe exceder 500 caracteres.");

            // Dirección requerida al registrar
            RuleFor(x => x.Direccion)
                .NotEmpty().WithMessage("La dirección es requerida.")
                .MaximumLength(500).WithMessage("La dirección no debe exceder 500 caracteres.");

            RuleFor(x => x.Latitud)
                .NotNull().WithMessage("La latitud es requerida.")
                .InclusiveBetween(-90, 90).WithMessage("La latitud debe estar entre -90 y 90.");

            RuleFor(x => x.Longitud)
                .NotNull().WithMessage("La longitud es requerida.")
                .InclusiveBetween(-180, 180).WithMessage("La longitud debe estar entre -180 y 180.");

            RuleFor(x => x.Alias)
                .MaximumLength(100).When(x => !string.IsNullOrEmpty(x.Alias))
                .WithMessage("El alias no debe exceder 100 caracteres.");
        }
    }
}