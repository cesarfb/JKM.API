using FluentValidation;
using JKM.APPLICATION.Aggregates;
using JKM.UTILITY.Utils;
using MediatR;

namespace JKM.APPLICATION.Queries.Proyecto.GetProyectoPaginado
{
    public class GetProyectoPaginadoQuery : IRequest<PaginadoResponse<ProyectoModel>>
    {
    }
}
