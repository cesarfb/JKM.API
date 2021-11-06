using MediatR;
using System.Threading;
using System.Threading.Tasks;
using JKM.PERSISTENCE.Repository.Cotizacion;
using JKM.UTILITY.Utils;

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
                email: request.Email, idCliente: request.IdCliente, idCotizacion: request.IdCotizacion, request.PrecioCotizacion,
                idTipoCotizacion: request.IdTipoCotizacion);

            return await _cotizacionRepository.UpdateCotizacion(cotizacion);
        }
    }
}
