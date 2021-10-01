using JKM.APPLICATION.Aggregates;
using JKM.PERSISTENCE.Utils;
using MediatR;
using System.Collections.Generic;

namespace JKM.APPLICATION.Queries.Trabajador.GetTrabajadoresPaginado
{
    public class GetTrabajadoresPaginadoQuery : PaginadoModel, IRequest<PaginadoResponse<TrabajadorModel>>
    {
        public int Estado { get; set; }
        public int Tipo { get; set; }
    }
}
