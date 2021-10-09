using FluentValidation;
using FluentValidation.Results;
using JKM.PERSISTENCE.Repository.Cotizacion;
using JKM.PERSISTENCE.Utils;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace JKM.APPLICATION.Commands.Cotizacion.AceptarCotizacion
{
    public class AceptarCotizacionCommandHandler : IRequestHandler<AceptarCotizacionCommand, ResponseModel>
    {
        private readonly ICotizacionRepository _cotizacionRepository;

        public AceptarCotizacionCommandHandler(ICotizacionRepository cotizacionRepository)
        {
            _cotizacionRepository = cotizacionRepository;
        }

        public async Task<ResponseModel> Handle(AceptarCotizacionCommand request, CancellationToken cancellationToken)
        {
            return await _cotizacionRepository.AceptarCotizacion(request.IdCotizacion);
        }
    }
}
