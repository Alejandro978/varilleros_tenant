namespace Varilleros.src.Application.Validators;

using FluentValidation;
using DTOs;

public class CreatePrecioDtoValidator : AbstractValidator<CreatePrecioDto>
{
    public CreatePrecioDtoValidator()
    {
        RuleFor(x => x.Numeroabolladuras).GreaterThan(0).WithMessage("Numeroabolladuras debe ser mayor a 0");
    }
}
