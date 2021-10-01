using JKM.APPLICATION.Aggregates;
using MediatR;
using System.Collections.Generic;

namespace JKM.APPLICATION.Queries.Venta.GetCuotasVentaById
{
    public class GetCuotasVentaByIdQuery : IRequest<IEnumerable<VentaCuotasModel>>
    {
        public int IdVenta { get; set; }
    }
}
