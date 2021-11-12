using FluentValidation;
using JKM.APPLICATION.Aggregates;
using JKM.UTILITY.Utils;
using MediatR;
using System.Collections.Generic;

namespace JKM.APPLICATION.Queries.Venta.GetCuotasVentaById
{
    public class GetCuotasVentaByIdQuery : IRequest<PaginadoResponse<VentaCuotasModel>>
    {
        public int IdVenta { get; set; }
    }

    public class Validator : AbstractValidator<GetCuotasVentaByIdQuery>
    {
        public Validator()
        {
            RuleFor(x => x.IdVenta)
                .GreaterThan(0).WithMessage("El idVenta debe ser un entero positivo");
        }
    }
}
