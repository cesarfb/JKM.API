using FluentValidation;
using JKM.APPLICATION.Aggregates;
using JKM.UTILITY.Utils;
using MediatR;

namespace JKM.APPLICATION.Queries.Trabajador.GetTrabajadoresPaginado
{
    public class GetTrabajadoresPaginadoQuery : IRequest<PaginadoResponse<TrabajadorModel>>
    {
        public int idEstado { get; set; }
        public int idTipo { get; set; }
    }
}
