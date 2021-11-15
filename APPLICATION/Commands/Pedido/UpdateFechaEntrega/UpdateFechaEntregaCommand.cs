using FluentValidation;
using JKM.UTILITY.Utils;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;
using System;

namespace JKM.APPLICATION.Commands.Pedido.UpdateFechaEntrega
{
    public class UpdateFechaEntregaCommand : IRequest<ResponseModel>
    {
        [SwaggerSchema(ReadOnly = true)]
        public int IdPedido { get; set; }
        public DateTime FechaEntrega { get; set; }
    }
    public class Validator : AbstractValidator<UpdateFechaEntregaCommand>
    {
        public Validator()
        {
        }
    }
}
