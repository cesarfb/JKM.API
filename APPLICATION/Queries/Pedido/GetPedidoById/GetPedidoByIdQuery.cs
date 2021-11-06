using FluentValidation;
using JKM.APPLICATION.Aggregates;
using MediatR;

namespace JKM.APPLICATION.Queries.Pedido.GetPedidoById
{
    public class GetPedidoByIdQuery : IRequest<PedidoModelFormat>
    {
        public int IdPedido { get; set; }
    }
    public class Validator : AbstractValidator<GetPedidoByIdQuery>
    {
        public Validator()
        {
            RuleFor(x => x.IdPedido)
                .GreaterThan(0).WithMessage("El IdPedido debe ser un entero positivo");
        }
    }
}
