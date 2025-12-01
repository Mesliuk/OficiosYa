using FluentValidation;
using OficiosYa.Api.Controllers;

namespace OficiosYa.Api.Validators
{
    public class UpdateClienteRequestValidator : AbstractValidator<UpdateClienteRequest>
    {
        public UpdateClienteRequestValidator()
        {
            RuleFor(x => x.Nombre).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Apellido).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Telefono).NotEmpty().MaximumLength(30);
        }
    }
}
