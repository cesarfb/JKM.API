using JKM.APPLICATION.Aggregates;
using MediatR;

namespace JKM.APPLICATION.Queries.Proyecto.GetProyectoById
{
    public class GetProyectoByIdQuery : IRequest<ProyectoModel>
    {
        public int IdProyecto { get; set; }

    }
}
