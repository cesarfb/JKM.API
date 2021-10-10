using JKM.UTILITY.Utils;
using MediatR;
using System.Collections.Generic;

namespace JKM.APPLICATION.Queries.Venta.GetTipoVenta
{
    public class GetTipoVentaQuery : IRequest<IEnumerable<Identifier>>
    {
    }
}
