using MediatR;
using Swashbuckle.AspNetCore.Annotations;
using FluentValidation;
using JKM.UTILITY.Utils;


namespace JKM.APPLICATION.Commands.Cotizacion.RegisterDetalleOrdenCotizacion
{
    public class RegisterDetalleOrdenCotizacionCommand : IRequest<ResponseModel>
    {
        [SwaggerSchema(ReadOnly = true)]
        public int IdCotizacion { get; set; }
        public int IdProducto { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }
    }

    public class Validator : AbstractValidator<RegisterDetalleOrdenCotizacionCommand>
    {
        public Validator()
        {
            RuleFor(x => x.IdCotizacion)
                .GreaterThan(0).WithMessage("El IdCotizacion debe ser un entero positivo");
            RuleFor(x => x.IdProducto)
                .GreaterThan(0).WithMessage("El IdProducto debe ser un entero positivo");
            RuleFor(x => x.Cantidad)
                .GreaterThan(0).WithMessage("La cantidad de trabajadores debe ser un entero positivo");
            RuleFor(x => x.Precio)
                .GreaterThan(0).WithMessage("El Precio debe ser un entero positivo");
        }
    }
}
