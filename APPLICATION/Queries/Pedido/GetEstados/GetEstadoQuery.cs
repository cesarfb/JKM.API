using JKM.UTILITY.Utils;
using MediatR;
using System.Collections.Generic;

namespace JKM.APPLICATION.Queries.Pedido.GetEstados
{
    public class GetEstadoQuery : IRequest<IEnumerable<Identifier>>
    {
    }
}
