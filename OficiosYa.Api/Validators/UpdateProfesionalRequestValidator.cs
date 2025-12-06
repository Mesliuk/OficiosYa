using FluentValidation;
using OficiosYa.Api.Controllers;
using OficiosYa.Api.Validators.Helpers;

namespace OficiosYa.Api.Validators
{
    public class UpdateProfesionalRequestValidator : AbstractValidator<UpdateProfesionalRequest>
    {
        public UpdateProfesionalRequestValidator()
        {
            RuleFor(x => x.Nombre).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Apellido).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Telefono).NotEmpty().MaximumLength(30);

            RuleFor(x => x.Documento)
                .NotEmpty()
                .Must(d => DocumentoHelper.IsValidArgentinianDocument(d)).WithMessage("Documento inválido para Argentina (DNI de 8 dígitos)");

            // Uniqueness should be validated in controller/handler where ProfesionalId is available; placeholder kept to ensure validation flow.
        }
    }
}
