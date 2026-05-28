namespace Varilleros.src.Application.Validators;

using FluentValidation;
using DTOs;

public class UpdateArticuloDtoValidator : AbstractValidator<UpdateArticuloDto>
{
    public UpdateArticuloDtoValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
        RuleFor(x => x.Codigo).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Descripcion).NotEmpty();
        RuleFor(x => x.CodigoPrecioPresupuesto).NotEmpty();
    }
}
