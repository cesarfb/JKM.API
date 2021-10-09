using JKM.PERSISTENCE.Utils;
using MediatR;
using System.Collections.Generic;

namespace JKM.APPLICATION.Queries.Cotizacion.GetEstadoCotizacion
{
    public class GetEstadoCotizacionQuery : IRequest<IEnumerable<Identifier>>
    {
    }
}
