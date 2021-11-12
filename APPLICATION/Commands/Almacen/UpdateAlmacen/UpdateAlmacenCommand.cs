using FluentValidation;
using JKM.UTILITY.Utils;
using MediatR;

namespace JKM.APPLICATION.Commands.Almacen.UpdateAlmacen
{
    public class UpdateAlmacenCommand : IRequest<ResponseModel>
    {
        public int IdAlmacen { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Distrito { get; set; }
    }

    public class Validator : AbstractValidator<UpdateAlmacenCommand>
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
