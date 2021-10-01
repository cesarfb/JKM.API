using JKM.APPLICATION.Aggregates;
using MediatR;

namespace JKM.APPLICATION.Queries.Venta.GetVentaById
{
    public class GetVentaByIdQuery : IRequest<VentaModel>
    {
        public int IdVenta { get; set; }
    }
}
