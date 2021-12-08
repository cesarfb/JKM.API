using FluentValidation;
using JKM.APPLICATION.Aggregates;
using MediatR;
using System.Collections.Generic;

namespace JKM.APPLICATION.Queries.Trabajador.GetTrabajadorDisponibleProyecto
{
    public class GetTrabajadorDisponibleProyectoQuery : IRequest<IEnumerable<TrabajadorModel>>
    {
    }
}
