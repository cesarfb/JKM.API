using JKM.APPLICATION.Aggregates;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JKM.APPLICATION.Queries.Cotizacion.GetActividadesByCotizacion
{
    public class GetActividadesByCotizacionQuery : IRequest<IEnumerable<ActividadCotizacionModel>>
    {
        public int IdCotizacion { get; set; }
    }
}
