namespace Varilleros.src.Application.Validators;

using FluentValidation;
using DTOs;

public class CreateClienteDtoValidator : AbstractValidator<CreateClienteDto>
{
    public CreateClienteDtoValidator()
    {
        RuleFor(x => x.NombreCliente)
            .NotEmpty().WithMessage("NombreCliente es obligatorio")
            .MaximumLength(200).WithMessage("NombreCliente no puede exceder 200 caracteres");
        RuleFor(x => x.NifCif)
            .NotEmpty().WithMessage("NifCif es obligatorio")
            .MaximumLength(20).WithMessage("NifCif no puede exceder 20 caracteres");
        RuleFor(x => x.Direccion).NotEmpty().WithMessage("Dirección es obligatoria");
        RuleFor(x => x.Poblacion).NotEmpty().WithMessage("Población es obligatoria");
        RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("Email inválido");
        RuleFor(x => x.Telefono).NotEmpty().WithMessage("Teléfono es obligatorio");
    }
}
