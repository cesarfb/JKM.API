using JKM.APPLICATION.Aggregates;
using JKM.PERSISTENCE.Utils;
using MediatR;
using System.Collections.Generic;

namespace JKM.APPLICATION.Queries.Trabajador.GetTrabajadorByEstado
{
    public class GetTrabajadorByEstadoQuery : IRequest<IEnumerable<TrabajadorModel>>
    {
        public int IdEstado { get; set; }
    }
}
