using FluentValidation;
using JKM.APPLICATION.Aggregates;
using MediatR;
using System.Collections.Generic;

namespace JKM.APPLICATION.Queries.Cotizacion.GetActividadesByCotizacion
{
    public class GetActividadesByCotizacionQuery : IRequest<IEnumerable<ActividadCotizacionModel>>
    {
        public int IdCotizacion { get; set; }
    }

    public class Validator : AbstractValidator<GetActividadesByCotizacionQuery>
    {
        public Validator()
        {
            RuleFor(x => x.IdCotizacion)
                .GreaterThan(0).WithMessage("El IdCotizacion debe ser un entero positivo");
        }
    }
}
