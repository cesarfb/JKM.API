using JKM.APPLICATION.Aggregates;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JKM.APPLICATION.Queries.Proyecto.GetTrabajadoresByProyecto
{
    public class GetTrabajadoresByProyectoQuery : IRequest<IEnumerable<TrabajadorProyectoModel>>
    {
        public int IdProyecto { get; set; }
    }
}
