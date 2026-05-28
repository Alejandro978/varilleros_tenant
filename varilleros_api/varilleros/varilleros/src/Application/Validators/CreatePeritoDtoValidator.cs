namespace Varilleros.src.Application.Validators;

using FluentValidation;
using DTOs;

public class CreatePeritoDtoValidator : AbstractValidator<CreatePeritoDto>
{
    public CreatePeritoDtoValidator()
    {
        RuleFor(x => x.Nombre).NotEmpty().MaximumLength(200);
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
    }
}
