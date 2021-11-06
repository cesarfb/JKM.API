using MediatR;
using FluentValidation;
using JKM.UTILITY.Utils;

namespace JKM.APPLICATION.Commands.Cliente.RegisterCliente
{
    public class RegisterClienteCommand : IRequest<ResponseModel>
    {
        public string RUC { get; set; }
        public string RazonSocial { get; set; }
        public string Telefono { get; set; }
    }

    public class Validator : AbstractValidator<RegisterClienteCommand>
    {
        public Validator()
        {
            RuleFor(x => x.RUC)
                .NotEmpty().WithMessage("El RUC no puede ser vacio")
                .Length(11).WithMessage("El RUC debe tener 11 caractéres");
            RuleFor(x => x.RazonSocial)
                .NotEmpty().WithMessage("La razon social no puede ser vacio");
            RuleFor(x => x.Telefono)
                .NotEmpty().WithMessage("El Telefono no puede ser vacio");
        }
    }
}
