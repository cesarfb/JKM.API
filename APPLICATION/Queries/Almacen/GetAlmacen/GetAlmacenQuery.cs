using JKM.APPLICATION.Aggregates;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JKM.APPLICATION.Queries.Almacen.GetAlmacen
{
    public class GetAlmacenQuery : IRequest<IEnumerable<AlmacenModel>>
    {
    }
}
