using JKM.PERSISTENCE.Repository.Cotizacion;
using JKM.PERSISTENCE.Repository.Proyecto;
using JKM.UTILITY.Utils;
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
            ProyectoModel model = new ProyectoModel();
            model.RegisterProyecto(nombreProyecto: request.Nombre, descripcion: request.Descripcion);

            return await _cotizacionRepository.AceptarCotizacion(request.IdCotizacion, model);
        }
    }
}
