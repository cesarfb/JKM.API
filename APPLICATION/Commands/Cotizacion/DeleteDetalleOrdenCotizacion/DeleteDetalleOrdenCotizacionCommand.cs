using MediatR;
using FluentValidation;
using JKM.UTILITY.Utils;

namespace JKM.APPLICATION.Commands.Cotizacion.DeleteDetalleOrdenCotizacion
{
    public class DeleteDetalleOrdenCotizacionCommand : IRequest<ResponseModel>
    {
        public int IdDetalleOrden { get; set; }
        public int IdCotizacion { get; set; }
    }
    public class Validator : AbstractValidator<DeleteDetalleOrdenCotizacionCommand>
    {
        public Validator()
        {
            RuleFor(x => x.IdCotizacion)
                .GreaterThan(0).WithMessage("El IdCotizacion debe ser un entero positivo");
            RuleFor(x => x.IdDetalleOrden)
                .GreaterThan(0).WithMessage("El IdDetalleOrden debe ser un entero positivo");
        }
    }
}
