namespace Varilleros.src.Application.Validators;

using FluentValidation;
using Varilleros.src.Domain.Entities;

public class UpdatePresupuestoDtoValidator : AbstractValidator<PresupuestoPayload>
{
    public UpdatePresupuestoDtoValidator()
    {
        RuleFor(x => x.Estado).InclusiveBetween((short)1, (short)4)
            .When(x => x.Estado.HasValue)
            .WithMessage("Estado debe ser 1=Borrador, 2=Enviado, 3=Aceptado, 4=Rechazado");
    }
}
