using JKM.APPLICATION.Aggregates;
using JKM.UTILITY.Utils;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JKM.APPLICATION.Queries.Servicio.GetServicioPaginado
{
    public class GetServicioPaginadoQuery : IRequest<PaginadoResponse<ServicioModel>>
    {
    }
}
