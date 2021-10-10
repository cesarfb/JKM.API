using MediatR;
using System.Threading;
using System.Threading.Tasks;
using JKM.PERSISTENCE.Repository.Cotizacion;
using JKM.UTILITY.Utils;

namespace JKM.APPLICATION.Commands.Cotizacion.DeleteActividadCotizacion
{
    public class DeleteActividadCotizacionCommandHandler : IRequestHandler<DeleteActividadCotizacionCommand, ResponseModel>
    {
        private readonly ICotizacionRepository _cotizacionRepository;

        public DeleteActividadCotizacionCommandHandler(ICotizacionRepository cotizacionRepository)
        {
            _cotizacionRepository = cotizacionRepository;
        }

        public async Task<ResponseModel> Handle(DeleteActividadCotizacionCommand request, CancellationToken cancellationToken)
        {
            return await _cotizacionRepository.DeleteActividadCotizacion(request.IdCotizacion, request.IdActividad);
        }
    }
}
