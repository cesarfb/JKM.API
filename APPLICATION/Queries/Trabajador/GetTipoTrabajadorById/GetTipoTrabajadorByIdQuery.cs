using JKM.APPLICATION.Aggregates;
using MediatR;

namespace JKM.APPLICATION.Queries.Trabajador.GetTipoTrabajadorById
{
    public class GetTipoTrabajadorByIdQuery : IRequest<TipoTrabajador>
    {
        public int IdTrabajador { get; set; }
    }
}
