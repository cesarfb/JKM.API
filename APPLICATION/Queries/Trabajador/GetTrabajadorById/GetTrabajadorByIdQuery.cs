using JKM.APPLICATION.Aggregates;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JKM.APPLICATION.Queries.Trabajador.GetTrabajadorById
{
    public class GetTrabajadorByIdQuery : IRequest<TrabajadorModel>
    {
        public int IdTrabajador { get; set; }
    }
}
