using JKM.UTILITY.Utils;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;

namespace JKM.APPLICATION.Commands.Producto.UpdateProducto
{
    public class UpdateProductoCommand : IRequest<ResponseModel>
    {
        [SwaggerSchema(ReadOnly = true)]
        public int IdProducto { get; set; }
        public string Nombre { get; set; }
        public string Codigo { get; set; }
    }
}
