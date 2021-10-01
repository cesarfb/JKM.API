using JKM.APPLICATION.Aggregates;
using JKM.PERSISTENCE.Utils;
using MediatR;
using System.Collections.Generic;

namespace JKM.APPLICATION.Queries.Cotizacion.GetCotizacionPaginado
{
    public class GetCotizacionPaginadoQuery : PaginadoModel, IRequest<PaginadoResponse<CotizacionModel>>
    {
    }
}
