using JKM.PERSISTENCE.Utils;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JKM.APPLICATION.Queries.Cotizacion.GetEstadoCotizacion
{
    public class GetEstadoCotizacionQuery : IRequest<IEnumerable<Identifier>>
    {
    }
}
