using JKM.APPLICATION.Aggregates;
using JKM.UTILITY.Utils;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JKM.APPLICATION.Queries.Pedido.GetPedido
{
    public class GetPedidoQuery : IRequest<IEnumerable<PedidoModel>>
    {
    }
}
