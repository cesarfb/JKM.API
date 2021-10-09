using FluentValidation;
using JKM.APPLICATION.Aggregates;
using MediatR;

namespace JKM.APPLICATION.Queries.Cotizacion.GetCotizacionById
{
    public class GetCotizacionByIdQuery : IRequest<CotizacionModel>
    {
        public int IdCotizacion { get; set; }
    }

    public class Validator : AbstractValidator<GetCotizacionByIdQuery>
    {
        public Validator()
        {
            RuleFor(x => x.IdCotizacion)
                .GreaterThan(0).WithMessage("El IdCotizacion debe ser un entero positivo");
        }
    }
}
