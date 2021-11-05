using JKM.UTILITY.Utils;
using MediatR;
using System.Collections.Generic;


namespace JKM.APPLICATION.Queries.Cotizacion.GetTipoCotizacion
{
    public class GetTipoCotizacionQuery : IRequest<IEnumerable<Identifier>>
    {
    }
}
