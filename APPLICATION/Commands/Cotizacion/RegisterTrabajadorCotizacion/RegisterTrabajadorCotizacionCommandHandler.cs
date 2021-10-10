using MediatR;
using System.Threading;
using System.Threading.Tasks;
using JKM.PERSISTENCE.Repository.Cotizacion;
using JKM.UTILITY.Utils;

namespace JKM.APPLICATION.Commands.Cotizacion.RegisterTrabajadorCotizacion
{
    public class RegisterTrabajadorCotizacionCommandHandler : IRequestHandler<RegisterTrabajadorCotizacionCommand, ResponseModel>
    {
        private readonly ICotizacionRepository _cotizacionRepository;

        public RegisterTrabajadorCotizacionCommandHandler(ICotizacionRepository cotizacionRepository)
        {
            _cotizacionRepository = cotizacionRepository;
        }

        public async Task<ResponseModel> Handle(RegisterTrabajadorCotizacionCommand request, CancellationToken cancellationToken)
        {
            TipoTrabajadorModel model = new TipoTrabajadorModel();
            model.RegisterTipoTrabajador(request.IdCotizacion, request.IdTipoTrabajador, request.Cantidad, request.Precio);

            return await _cotizacionRepository.RegisterTrabajadorCotizacion(model);
        }
    }
}
