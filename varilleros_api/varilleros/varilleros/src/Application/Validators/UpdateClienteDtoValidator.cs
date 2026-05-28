namespace Varilleros.src.Application.Validators;

using FluentValidation;
using DTOs;

public class UpdateClienteDtoValidator : AbstractValidator<UpdateClienteDto>
{
    public UpdateClienteDtoValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).WithMessage("Id debe ser válido");
        RuleFor(x => x.NombreCliente).NotEmpty().MaximumLength(200);
        RuleFor(x => x.NifCif).NotEmpty().MaximumLength(20);
        RuleFor(x => x.Direccion).NotEmpty();
        RuleFor(x => x.Poblacion).NotEmpty();
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Telefono).NotEmpty();
    }
}
