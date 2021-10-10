using MediatR;
using System.Threading;
using System.Threading.Tasks;
using JKM.PERSISTENCE.Repository.Cotizacion;
using JKM.UTILITY.Utils;

namespace JKM.APPLICATION.Commands.Cotizacion.RegisterActividadCotizacion
{
    public class RegisterActividadCotizacionCommandHandler : IRequestHandler<RegisterActividadCotizacionCommand, ResponseModel>
    {
        private readonly ICotizacionRepository _cotizacionRepository;

        public RegisterActividadCotizacionCommandHandler(ICotizacionRepository cotizacionRepository)
        {
            _cotizacionRepository = cotizacionRepository;
        }

        public async Task<ResponseModel> Handle(RegisterActividadCotizacionCommand request, CancellationToken cancellationToken)
        {
            ActividadCotizacionModel model = new ActividadCotizacionModel();
            model.RegisterActividad(descripcion: request.Descripcion, peso: request.Peso,
                idPadre: request.IdPadre, idHermano: request.IdHermano, idCotizacion: request.IdCotizacion);

            return await _cotizacionRepository.RegisterActividadCotizacion(model);
        }
    }
}
