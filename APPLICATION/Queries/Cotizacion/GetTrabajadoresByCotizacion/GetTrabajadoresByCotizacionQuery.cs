using JKM.APPLICATION.Aggregates;
using MediatR;
using System.Collections.Generic;

namespace JKM.APPLICATION.Queries.Cotizacion.GetTrabajadoresByCotizacion
{
    public class GetTrabajadoresByCotizacionQuery : IRequest<IEnumerable<TipoTrabajadorModel>>
    {
        public int IdCotizacion { get; set; }
    }
}
