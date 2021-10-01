using JKM.PERSISTENCE.Utils;
using MediatR;
using System.Collections.Generic;

namespace JKM.APPLICATION.Queries.Venta.GetEstadoVenta
{
    public class GetEstadoVentaQuery : IRequest<IEnumerable<Identifier>>
    {
    }
}
