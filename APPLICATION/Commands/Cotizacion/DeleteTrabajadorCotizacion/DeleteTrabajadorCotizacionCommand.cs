using MediatR;
using JKM.PERSISTENCE.Utils;
using FluentValidation;

namespace JKM.APPLICATION.Commands.Cotizacion.DeleteTrabajadorCotizacion
{
    public class DeleteTrabajadorCotizacionCommand : IRequest<ResponseModel>
    {
        public int IdTipoTrabajador{ get; set; }
        public int IdCotizacion { get; set; }
    }
    public class Validator : AbstractValidator<DeleteTrabajadorCotizacionCommand>
    {
        public Validator()
        {
            RuleFor(x => x.IdCotizacion)
                .GreaterThan(0).WithMessage("El IdCotizacion debe ser un entero positivo");
            RuleFor(x => x.IdTipoTrabajador)
                .GreaterThan(0).WithMessage("El IdTipo debe ser un entero positivo");
        }
    }
}
