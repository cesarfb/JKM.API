using JKM.PERSISTENCE.Repository.Producto;
using JKM.UTILITY.Utils;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace JKM.APPLICATION.Commands.Producto.RegisterProducto
{
    public class RegisterProductoCommandHandler : IRequestHandler<RegisterProductoCommand, ResponseModel>
    {
        private readonly IProductoRepository _productoRepository;

        public RegisterProductoCommandHandler(IProductoRepository productoRepository)
        {
            _productoRepository = productoRepository;
        }

        public async Task<ResponseModel> Handle(RegisterProductoCommand request, CancellationToken cancellationToken)
        {
            ProductoModel model = new ProductoModel();
            model.RegisterProducto(model.Nombre, model.Codigo);
            return await _productoRepository.RegisterProducto(model);
        }
    }
}
