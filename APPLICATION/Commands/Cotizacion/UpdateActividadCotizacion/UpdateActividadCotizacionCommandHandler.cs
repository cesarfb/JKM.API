using MediatR;
using System.Threading;
using System.Threading.Tasks;
using JKM.PERSISTENCE.Repository.Cotizacion;
using JKM.UTILITY.Utils;

namespace JKM.APPLICATION.Commands.Cotizacion.UpdateActividadCotizacion
{
    public class UpdateActividadCotizacionCommandHandler : IRequestHandler<UpdateActividadCotizacionCommand, ResponseModel>
    {
        private readonly ICotizacionRepository _cotizacionRepository;

        public UpdateActividadCotizacionCommandHandler(ICotizacionRepository cotizacionRepository)
        {
            _cotizacionRepository = cotizacionRepository;
        }

        public async Task<ResponseModel> Handle(UpdateActividadCotizacionCommand request, CancellationToken cancellationToken)
        {
            ActividadCotizacionModel model = new ActividadCotizacionModel();
            model.UpdateActividad(descripcion: request.Descripcion, peso: request.Peso,
                idPadre: request.IdPadre, idHermano: request.IdHermano, idActividad: request.IdActividad, idCotizacion: request.IdCotizacion);

            return await _cotizacionRepository.UpdateActividadCotizacion(model);
        }
    }
}
