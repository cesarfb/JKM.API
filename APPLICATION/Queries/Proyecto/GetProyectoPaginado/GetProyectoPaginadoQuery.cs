using JKM.APPLICATION.Aggregates;
using JKM.PERSISTENCE.Utils;
using MediatR;

namespace JKM.APPLICATION.Queries.Proyecto.GetProyectoPaginado
{
    public class GetProyectoPaginadoQuery : PaginadoModel, IRequest<PaginadoResponse<ProyectoModel>>
    {
    }
}
