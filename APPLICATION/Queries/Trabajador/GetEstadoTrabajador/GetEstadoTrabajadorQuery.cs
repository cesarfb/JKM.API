using JKM.PERSISTENCE.Utils;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JKM.APPLICATION.Queries.Trabajador.GetEstadoTrabajador
{
    public class GetEstadoTrabajadorQuery : IRequest<IEnumerable<Identifier>>
    {
    }
}
