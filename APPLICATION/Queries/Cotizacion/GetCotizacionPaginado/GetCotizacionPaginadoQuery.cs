using FluentValidation;
using JKM.APPLICATION.Aggregates;
using JKM.PERSISTENCE.Utils;
using MediatR;
using System.Collections.Generic;

namespace JKM.APPLICATION.Queries.Cotizacion.GetCotizacionPaginado
{
    public class GetCotizacionPaginadoQuery : PaginadoModel, IRequest<PaginadoResponse<CotizacionModel>>
    {
    }

    public class Validator : AbstractValidator<GetCotizacionPaginadoQuery>
    {
        public Validator()
        {
            RuleFor(x => x.Pages)
                .GreaterThan(0).WithMessage("La cantidad de paginas debe ser un entero positivo");
            RuleFor(x => x.Rows)
                .GreaterThan(0).WithMessage("La cantidad de registros debe ser un entero positivo");
        }
    }
}
