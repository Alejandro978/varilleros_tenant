namespace Varilleros.src.Application.Validators;

using FluentValidation;
using DTOs;

public class CreateTenantDtoValidator : AbstractValidator<CreateTenantDto>
{
    public CreateTenantDtoValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(200);
        RuleFor(x => x.Slug).NotEmpty().MaximumLength(100)
            .Matches("^[a-z0-9-]+$").WithMessage("Slug solo puede contener letras minúsculas, números y guiones");
        RuleFor(x => x.DbHost).NotEmpty();
        RuleFor(x => x.DbPort).InclusiveBetween(1, 65535);
        RuleFor(x => x.DbName).NotEmpty();
        RuleFor(x => x.DbUser).NotEmpty();
        RuleFor(x => x.AppPassword).NotEmpty().MinimumLength(6);
    }
}
