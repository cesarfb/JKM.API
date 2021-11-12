using FluentValidation;
using JKM.UTILITY.Utils;
using MediatR;

namespace JKM.APPLICATION.Commands.Almacen.RegisterAlmacen
{
    public class RegisterAlmacenCommand : IRequest<ResponseModel>
    {
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Distrito { get; set; }
    }

    public class Validator : AbstractValidator<RegisterAlmacenCommand>
    {
        public Validator()
        {
            RuleFor(x => x.Nombre)
                .NotEmpty().WithMessage("El nonmbre no puede ser vacío");
            RuleFor(x => x.Direccion)
              .NotEmpty().WithMessage("La direccion no puede ser vacío");
            RuleFor(x => x.Distrito)
              .NotEmpty().WithMessage("El distrito no puede ser vacío");
        }
    }
}
