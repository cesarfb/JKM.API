using JKM.APPLICATION.Aggregates;
using MediatR;
using System.Collections.Generic;

namespace JKM.APPLICATION.Queries.WebCliente.GetServicios
{
    public class GetServiciosQuery : IRequest<IEnumerable<ServicioWebModel>>
    {
    }
}
