using FluentValidation;
using JKM.UTILITY.Utils;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Text;

namespace JKM.APPLICATION.Commands.Venta.DeleteVenta
{
    public class DeleteVentaCommand : IRequest<ResponseModel>
    {
        [SwaggerSchema(ReadOnly = true)]
        public int IdVenta { get; set; }
    }

    public class Validator : AbstractValidator<DeleteVentaCommand>
    {
        public Validator()
        {
            RuleFor(x => x.IdVenta).GreaterThan(0).WithMessage("El IdVenta debe ser un entero positivo");
        }
    }
}
