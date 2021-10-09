using FluentValidation;
using JKM.APPLICATION.Aggregates;
using MediatR;

namespace JKM.APPLICATION.Queries.Venta.GetVentaById
{
    public class GetVentaByIdQuery : IRequest<VentaModel>
    {
        public int IdVenta { get; set; }
    }
    public class Validator : AbstractValidator<GetVentaByIdQuery>
    {
        public Validator()
        {
            RuleFor(x => x.IdVenta)
                .GreaterThan(0).WithMessage("El idVenta debe ser un entero positivo");
        }
    }
}
