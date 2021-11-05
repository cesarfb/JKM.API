using FluentValidation;
using JKM.APPLICATION.Aggregates;
using JKM.UTILITY.Utils;
using MediatR;
using System.Collections.Generic;

namespace JKM.APPLICATION.Queries.Producto.GetProducto
{
    public class GetProductoQuery : IRequest<IEnumerable<ProductoModel>>
    {
    }
}
