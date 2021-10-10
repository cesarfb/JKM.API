using JKM.UTILITY.Utils;
using MediatR;
using System.Collections.Generic;

namespace JKM.APPLICATION.Queries.Proyecto.GetEstadosProyecto
{
    public class GetEstadosProyectoQuery : IRequest<IEnumerable<Identifier>>
    {
    }
}
