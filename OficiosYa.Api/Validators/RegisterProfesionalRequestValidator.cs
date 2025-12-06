using FluentValidation;
using OficiosYa.Api.Models;
using OficiosYa.Application.Interfaces;
using OficiosYa.Domain.Enums;

namespace OficiosYa.Api.Validators
{
    public class RegisterProfesionalRequestValidator : AbstractValidator<RegisterProfesionalRequest>
    {
        public RegisterProfesionalRequestValidator(IOficioRepository oficioRepo, IRubroRepository rubroRepo, IUsuarioRepository usuarioRepo, IProfesionalRepository profesionalRepo)
        {
            RuleFor(x => x.Nombre).NameRule();
            RuleFor(x => x.Apellido).LastNameRule();

            RuleFor(x => x.Correo)
                .EmailFormatRule()
                .MustAsync(async (email, ct) =>
                {
                    // Allow same email if it's registered as Cliente but not if already registered as Profesional
                    var existingProfesional = await usuarioRepo.ObtenerPorEmailYRolAsync(email, UsuarioRoleEnum.Profesional);
                    return existingProfesional == null;
                }).WithMessage("El correo ya está registrado para un profesional");

            RuleFor(x => x.Telefono).PhoneFormatRule();
            RuleFor(x => x.Password).PasswordStrengthRule();

            RuleFor(x => x.Documento).NotEmpty().MaximumLength(50).WithMessage("Documento requerido");

            RuleFor(x => x.OficioId).GreaterThan(0).MustAsync(async (id, ct) =>
            {
                var oficio = await oficioRepo.ObtenerPorIdAsync(id);
                return oficio != null;
            }).WithMessage("OficioId no existe");

            RuleFor(x => x.RubroId).GreaterThan(0).MustAsync(async (id, ct) =>
            {
                var rubro = await rubroRepo.ObtenerPorIdConOficiosAsync(id);
                return rubro != null;
            }).WithMessage("RubroId no existe");
        }
    }
}
