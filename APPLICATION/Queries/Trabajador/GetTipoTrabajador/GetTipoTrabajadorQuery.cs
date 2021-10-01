using JKM.APPLICATION.Aggregates;
using MediatR;
using System.Collections.Generic;

namespace JKM.APPLICATION.Queries.Trabajador.GetTipoTrabajador
{
    public class GetTipoTrabajadorQuery : IRequest<IEnumerable<TipoTrabajador>>
    {
    }
}
