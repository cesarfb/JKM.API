using JKM.APPLICATION.Aggregates;
using JKM.PERSISTENCE.Utils;
using MediatR;

namespace JKM.APPLICATION.Queries.Venta.GetVentaPaginado
{
    public class GetVentaPaginadoQuery : PaginadoModel, IRequest<PaginadoResponse<VentaModel>>
    {
    }
}
