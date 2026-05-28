namespace Varilleros.src.Application.Validators;

using FluentValidation;
using DTOs;

public class CreateArticuloDtoValidator : AbstractValidator<CreateArticuloDto>
{
    public CreateArticuloDtoValidator()
    {
        RuleFor(x => x.Codigo).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Descripcion).NotEmpty();
        RuleFor(x => x.CodigoPrecioPresupuesto).NotEmpty();
    }
}
