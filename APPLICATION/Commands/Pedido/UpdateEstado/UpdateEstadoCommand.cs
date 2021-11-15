using FluentValidation;
using JKM.UTILITY.Utils;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;

namespace JKM.APPLICATION.Commands.Pedido.UpdateEstado
{
    public class UpdateEstadoCommand : IRequest<ResponseModel>
    {
        [SwaggerSchema(ReadOnly = true)]
        public int IdPedido { get; set; }
        public int IdEstado { get; set; }
    }
    public class Validator : AbstractValidator<UpdateEstadoCommand>
    {
        public Validator()
        {
            RuleFor(x => x.IdEstado)
                .GreaterThan(0).WithMessage("El IdEstado debe ser un entero positivo");
        }
    }
}
