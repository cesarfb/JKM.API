using MediatR;
using System.Threading;
using System.Threading.Tasks;
using JKM.PERSISTENCE.Repository.Cotizacion;
using JKM.UTILITY.Utils;

namespace JKM.APPLICATION.Commands.Cotizacion.UpdateDetalleOrdenCotizacion
{
    public class UpdateDetalleOrdenCotizacionCommandHandler : IRequestHandler<UpdateDetalleOrdenCotizacionCommand, ResponseModel>
    {
        private readonly ICotizacionRepository _cotizacionRepository;

        public UpdateDetalleOrdenCotizacionCommandHandler(ICotizacionRepository cotizacionRepository)
        {
            _cotizacionRepository = cotizacionRepository;
        }

        public async Task<ResponseModel> Handle(UpdateDetalleOrdenCotizacionCommand request, CancellationToken cancellationToken)
        {
            DetalleOrdenCotizacionModel model = new DetalleOrdenCotizacionModel();
            model.UpdateDetalleOrden(idCotizacion: request.IdCotizacion, idDetalleOrden: request.IdDetalleOrden,
                cantidad: request.Cantidad, precio: request.Precio);
            return await _cotizacionRepository.UpdateDetalleOrdenCotizacion(model);
        }
    }
}
