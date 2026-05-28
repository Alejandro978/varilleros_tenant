namespace Varilleros.src.Application.Validators;

using FluentValidation;
using DTOs;

public class CreatePrecioDtoValidator : AbstractValidator<CreatePrecioDto>
{
    public CreatePrecioDtoValidator()
    {
        RuleFor(x => x.NumeroabolladuraS).GreaterThan(0).WithMessage("NumeroabolladuraS debe ser mayor a 0");
    }
}
