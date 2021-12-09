using JKM.PERSISTENCE.Repository.Producto;
using JKM.UTILITY.Utils;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace JKM.APPLICATION.Commands.Producto.UpdateProducto
{
    public class UpdateProductoCommandHandler : IRequestHandler<UpdateProductoCommand, ResponseModel>
    {
        private readonly IProductoRepository _productoRepository;

        public UpdateProductoCommandHandler(IProductoRepository productoRepository)
        {
            _productoRepository = productoRepository;
        }

        public async Task<ResponseModel> Handle(UpdateProductoCommand request, CancellationToken cancellationToken)
        {
            ProductoModel model = new ProductoModel();
            model.UpdateProducto(request.IdProducto, request.Nombre, request.Codigo, request.Imagen);
            return await _productoRepository.UpdateProducto(model);
        }
    }
}
