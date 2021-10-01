using JKM.PERSISTENCE.Utils;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JKM.APPLICATION.Queries.Proyecto.GetEstadosProyecto
{
    public class GetEstadosProyectoQuery : IRequest<IEnumerable<Identifier>>
    {
    }
}
