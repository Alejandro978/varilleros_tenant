namespace Varilleros.src.Application.Validators;

using FluentValidation;
using DTOs;

public class UpdatePresupuestoDtoValidator : AbstractValidator<UpdatePresupuestoDto>
{
    public UpdatePresupuestoDtoValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
        RuleFor(x => x.Descripcion).NotEmpty();
        RuleFor(x => x.TotalPresupuesto).GreaterThanOrEqualTo(0);
        RuleFor(x => x.Estado).NotEmpty();
    }
}
