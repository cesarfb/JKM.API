using MediatR;
using Swashbuckle.AspNetCore.Annotations;
using FluentValidation;
using JKM.UTILITY.Utils;

namespace JKM.APPLICATION.Commands.Cotizacion.UpdateDetalleOrdenCotizacion
{
    public class UpdateDetalleOrdenCotizacionCommand : IRequest<ResponseModel>
    {
        [SwaggerSchema(ReadOnly = true)]
        public int IdCotizacion { get; set; }
        [SwaggerSchema(ReadOnly = true)]
        public int IdDetalleOrden { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }
    }

    public class Validator : AbstractValidator<UpdateDetalleOrdenCotizacionCommand>
    {
        public Validator()
        {
            RuleFor(x => x.IdCotizacion)
                .GreaterThan(0).WithMessage("El IdCotizacion debe ser un entero positivo");
            RuleFor(x => x.IdDetalleOrden)
                .GreaterThan(0).WithMessage("El IdDetalleOrden debe ser un entero positivo");
            RuleFor(x => x.Cantidad)
                .GreaterThan(0).WithMessage("La cantidad de trabajadores debe ser un entero positivo");
            RuleFor(x => x.Precio)
                .GreaterThan(0).WithMessage("El Precio debe ser un entero positivo");
        }
    }
}
