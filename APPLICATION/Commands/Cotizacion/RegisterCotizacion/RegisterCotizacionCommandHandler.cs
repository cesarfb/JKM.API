using MediatR;
using System.Threading;
using System.Threading.Tasks;
using JKM.PERSISTENCE.Repository.Cotizacion;
using JKM.UTILITY.Utils;

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
                email: request.Email, idCliente: request.IdCliente, precioCotizacion: request.PrecioCotizacion,
                idTipoCotizacion: request.IdTipoCotizacion);
            return await _cotizacionRepository.RegisterCotizacion(model);
        }  
    }
}
