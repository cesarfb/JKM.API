using MediatR;
using System.Threading;
using System.Threading.Tasks;
using JKM.PERSISTENCE.Repository.Cotizacion;
using JKM.UTILITY.Utils;

namespace JKM.APPLICATION.Commands.Cotizacion.RechazarCotizacion
{
    public class RechazarCotizacionCommandHandler : IRequestHandler<RechazarCotizacionCommand, ResponseModel>
    {
        private readonly ICotizacionRepository _cotizacionRepository;

        public RechazarCotizacionCommandHandler(ICotizacionRepository cotizacionRepository)
        {
            _cotizacionRepository = cotizacionRepository;
        }
        
        public async Task<ResponseModel> Handle(RechazarCotizacionCommand request, CancellationToken cancellationToken)
        {
           return await _cotizacionRepository.RechazarCotizacion(request.IdCotizacion);
        }
    }
}
