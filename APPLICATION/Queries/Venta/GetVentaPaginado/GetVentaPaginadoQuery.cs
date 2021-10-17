using FluentValidation;
using JKM.APPLICATION.Aggregates;
using JKM.UTILITY.Utils;
using MediatR;

namespace JKM.APPLICATION.Queries.Venta.GetVentaPaginado
{
    public class GetVentaPaginadoQuery : IRequest<PaginadoResponse<VentaModel>>
    {
    }
}
