using FluentValidation;
using JKM.APPLICATION.Aggregates;
using MediatR;
using System.Collections.Generic;

namespace JKM.APPLICATION.Queries.Cotizacion.GetTrabajadoresByCotizacion
{
    public class GetTrabajadoresByCotizacionQuery : IRequest<IEnumerable<TipoTrabajadorModel>>
    {
        public int IdCotizacion { get; set; }
    }

    public class Validator : AbstractValidator<GetTrabajadoresByCotizacionQuery>
    {
        public Validator()
        {
            RuleFor(x => x.IdCotizacion)
                .GreaterThan(0).WithMessage("El IdCotizacion debe ser un entero positivo");
        }
    }
}
