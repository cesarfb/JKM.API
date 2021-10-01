using JKM.APPLICATION.Aggregates;
using MediatR;
using System.Collections.Generic;

namespace JKM.APPLICATION.Queries.Proyecto.GetActividadesByProyecto
{
    public class GetActividadesByProyectoQuery : IRequest<IEnumerable<ActividadProyectoModel>>
    {
        public int IdProyecto { get; set; }
    }
}
