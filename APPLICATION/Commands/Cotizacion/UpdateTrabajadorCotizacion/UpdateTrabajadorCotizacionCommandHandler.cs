using MediatR;
using System.Threading;
using JKM.PERSISTENCE.Utils;
using System.Threading.Tasks;
using JKM.PERSISTENCE.Repository.Cotizacion;

namespace JKM.APPLICATION.Commands.Cotizacion.UpdateTrabajadorCotizacion
{
    public class UpdateTrabajadorCotizacionCommandHandler : IRequestHandler<UpdateTrabajadorCotizacionCommand, ResponseModel>
    {
        private readonly ICotizacionRepository _cotizacionRepository;

        public UpdateTrabajadorCotizacionCommandHandler(ICotizacionRepository cotizacionRepository)
        {
            _cotizacionRepository = cotizacionRepository;
        }

        public async Task<ResponseModel> Handle(UpdateTrabajadorCotizacionCommand request, CancellationToken cancellationToken)
        {
            TipoTrabajadorModel model = new TipoTrabajadorModel();
            model.UpdateTipoTrabajador(request.IdCotizacion, request.IdTipoTrabajador, request.Cantidad, request.Precio);
            return await _cotizacionRepository.UpdateTrabajadorCotizacion(model);
        }
    }
}