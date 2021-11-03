using FluentValidation;
using JKM.APPLICATION.Aggregates;
using JKM.UTILITY.Utils;
using MediatR;
using System.Collections.Generic;

namespace JKM.APPLICATION.Queries.Cotizacion.GetCotizacionPaginado
{
    public class GetCotizacion : IRequest<IEnumerable<CotizacionModel>>
    {
    }

    public class Validator : AbstractValidator<GetCotizacion>
    {
        public Validator()
        {
        }
    }
}
