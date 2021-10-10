using MediatR;
using System.Threading;
using System.Threading.Tasks;
using JKM.PERSISTENCE.Repository.Cotizacion;
using JKM.UTILITY.Utils;

namespace JKM.APPLICATION.Commands.Cotizacion.DeleteTrabajadorCotizacion
{
    public class DeleteTrabajadorCotizacionCommandHanlder : IRequestHandler<DeleteTrabajadorCotizacionCommand, ResponseModel>
    {
        private readonly ICotizacionRepository _cotizacionRepository;

        public DeleteTrabajadorCotizacionCommandHanlder(ICotizacionRepository cotizacionRepository)
        {
            _cotizacionRepository = cotizacionRepository;
        }

        public async Task<ResponseModel> Handle(DeleteTrabajadorCotizacionCommand request, CancellationToken cancellationToken)
        {
            return await _cotizacionRepository.DeleteTrabajadorCotizacion(request.IdCotizacion, request.IdTipoTrabajador);
        }
    }
}
