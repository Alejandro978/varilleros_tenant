namespace Varilleros.src.Application.Validators;

using FluentValidation;
using DTOs;

public class CreateModuleCatalogDtoValidator : AbstractValidator<CreateModuleCatalogDto>
{
    public CreateModuleCatalogDtoValidator()
    {
        RuleFor(x => x.Code).NotEmpty().MaximumLength(50);
        RuleFor(x => x.Name).NotEmpty().MaximumLength(200);
        RuleFor(x => x.Description).NotEmpty();
    }
}
