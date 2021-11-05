using MediatR;
using System.Threading;
using System.Threading.Tasks;
using JKM.PERSISTENCE.Repository.Cotizacion;
using JKM.UTILITY.Utils;

namespace JKM.APPLICATION.Commands.Cotizacion.RegisterDetalleOrdenCotizacion
{
    public class RegisterDetalleOrdenCotizacionCommandHandler : IRequestHandler<RegisterDetalleOrdenCotizacionCommand, ResponseModel>
    {
        private readonly ICotizacionRepository _cotizacionRepository;

        public RegisterDetalleOrdenCotizacionCommandHandler(ICotizacionRepository cotizacionRepository)
        {
            _cotizacionRepository = cotizacionRepository;
        }

        public async Task<ResponseModel> Handle(RegisterDetalleOrdenCotizacionCommand request, CancellationToken cancellationToken)
        {
            DetalleOrdenCotizacionModel model = new DetalleOrdenCotizacionModel();
            model.RegisterDetalleOrden(idCotizacion: request.IdCotizacion, idProducto: request.IdProducto, cantidad:
                request.Cantidad, precio: request.Precio);

            return await _cotizacionRepository.RegisterDetalleOrdenCotizacion(model);
        }
    }
}
