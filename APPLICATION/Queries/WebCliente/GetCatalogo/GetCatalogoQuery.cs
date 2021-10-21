using JKM.APPLICATION.Aggregates;
using MediatR;
using System.Collections.Generic;

namespace JKM.APPLICATION.Queries.WebCliente.GetProductos
{
    public class GetCatalogoQuery : IRequest<IEnumerable<CatalogoWebModel>>
    {

    }
}
