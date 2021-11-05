using MediatR;
using System.Threading;
using System.Threading.Tasks;
using JKM.PERSISTENCE.Repository.Cotizacion;
using JKM.UTILITY.Utils;

namespace JKM.APPLICATION.Commands.Cotizacion.DeleteDetalleOrdenCotizacion
{
    public class DeleteDetalleOrdenCotizacionCommandHandler : IRequestHandler<DeleteDetalleOrdenCotizacionCommand, ResponseModel>
    {
        private readonly ICotizacionRepository _cotizacionRepository;

        public DeleteDetalleOrdenCotizacionCommandHandler(ICotizacionRepository cotizacionRepository)
        {
            _cotizacionRepository = cotizacionRepository;
        }

        public async Task<ResponseModel> Handle(DeleteDetalleOrdenCotizacionCommand request, CancellationToken cancellationToken)
        {
            return await _cotizacionRepository.DeleteDetalleOrdenCotizacion(request.IdCotizacion, request.IdDetalleOrden);
        }
    }
}
