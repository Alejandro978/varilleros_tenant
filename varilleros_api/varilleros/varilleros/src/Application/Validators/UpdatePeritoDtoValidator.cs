namespace Varilleros.src.Application.Validators;

using FluentValidation;
using DTOs;

public class UpdatePeritoDtoValidator : AbstractValidator<UpdatePeritoDto>
{
    public UpdatePeritoDtoValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);
        RuleFor(x => x.Nombre).NotEmpty().MaximumLength(200);
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
    }
}
