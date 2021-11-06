using FluentValidation;
using JKM.APPLICATION.Aggregates;
using MediatR;
using System.Collections.Generic;

namespace JKM.APPLICATION.Queries.Cotizacion.GetDetalleOrdenByCotizacion
{
    public class GetDetalleOrdenByCotizacionQuery : IRequest<IEnumerable<DetalleOrdenModel>>
    {
        public int IdCotizacion { get; set; }
    }

    public class Validator : AbstractValidator<GetDetalleOrdenByCotizacionQuery>
    {
        public Validator()
        {
            RuleFor(x => x.IdCotizacion)
                .GreaterThan(0).WithMessage("El IdCotizacion debe ser un entero positivo");
        }
    }
}
