using JKM.UTILITY.Utils;
using MediatR;
using System.Collections.Generic;

namespace JKM.APPLICATION.Queries.Trabajador.GetEstadoTrabajador
{
    public class GetEstadoTrabajadorQuery : IRequest<IEnumerable<Identifier>>
    {
    }
}
