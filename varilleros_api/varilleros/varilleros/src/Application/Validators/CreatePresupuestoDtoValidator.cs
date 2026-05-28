namespace Varilleros.src.Application.Validators;

using FluentValidation;
using DTOs;

public class CreatePresupuestoDtoValidator : AbstractValidator<CreatePresupuestoDto>
{
    public CreatePresupuestoDtoValidator()
    {
        RuleFor(x => x.ClienteId).GreaterThan(0);
        RuleFor(x => x.PeritoId).GreaterThan(0);
        RuleFor(x => x.Descripcion).NotEmpty();
        RuleFor(x => x.TotalPresupuesto).GreaterThanOrEqualTo(0);
    }
}
