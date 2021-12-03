using JKM.UTILITY.Utils;
using MediatR;

namespace JKM.APPLICATION.Commands.Producto.RegisterProducto
{
    public class RegisterProductoCommand : IRequest<ResponseModel>
    {
        public string Nombre { get; set; }
        public string Codigo { get; set; }
    }
}
