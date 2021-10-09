using MediatR;
using System.Threading;
using JKM.PERSISTENCE.Utils;
using System.Threading.Tasks;
using JKM.PERSISTENCE.Repository.Cotizacion;

namespace JKM.APPLICATION.Commands.Cotizacion.RegisterCotizacion
{
    public class RegisterCotizacionCommandHandler : IRequestHandler<RegisterCotizacionCommand, ResponseModel>
    {
        private readonly ICotizacionRepository _cotizacionRepository;

        public RegisterCotizacionCommandHandler(ICotizacionRepository cotizacionRepository)
        {
            _cotizacionRepository = cotizacionRepository;
        }

        public async Task<ResponseModel> Handle(RegisterCotizacionCommand request, CancellationToken cancellationToken)
        {
            CotizacionModel model = new CotizacionModel();
            model.RegisterCotizacion(solicitante: request.Solicitante, fechaSolicitud: request.FechaSolicitud, descripcion: request.Descripcion,
                email: request.Email, empresa: request.Empresa);
            return await _cotizacionRepository.RegisterCotizacion(model);
        }  
    }
}
