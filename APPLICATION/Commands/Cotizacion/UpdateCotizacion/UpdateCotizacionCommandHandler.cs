using MediatR;
using System.Threading;
using JKM.PERSISTENCE.Utils;
using System.Threading.Tasks;
using JKM.PERSISTENCE.Repository.Cotizacion;

namespace JKM.APPLICATION.Commands.Cotizacion.UpdateCotizacion
{
    public class UpdateCotizacionCommandHandler : IRequestHandler<UpdateCotizacionCommand, ResponseModel>
    {
        private readonly ICotizacionRepository _cotizacionRepository;

        public UpdateCotizacionCommandHandler(ICotizacionRepository cotizacionRepository)
        {
            _cotizacionRepository = cotizacionRepository;
        }

        public async Task<ResponseModel> Handle(UpdateCotizacionCommand request, CancellationToken cancellationToken)
        {
            CotizacionModel cotizacion = new CotizacionModel();
            cotizacion.UpdateCotizacion(solicitante: request.Solicitante, fechaSolicitud: request.FechaSolicitud, descripcion: request.Descripcion, 
                email: request.Email,empresa: request.Empresa, idCotizacion: request.IdCotizacion, idEstado: request.IdEstado, 
                request.IdPrecioCotizacion, request.PrecioCotizacion);

            return await _cotizacionRepository.UpdateCotizacion(cotizacion);
        }
    }
}
