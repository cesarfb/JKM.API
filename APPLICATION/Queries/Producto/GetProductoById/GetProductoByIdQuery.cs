using JKM.APPLICATION.Aggregates;
using MediatR;

namespace JKM.APPLICATION.Queries.Producto.GetProductoById
{
    public class GetProductoByIdQuery : IRequest<ProductoModel>
    {
        public int IdProducto { get; set; }
    }
}