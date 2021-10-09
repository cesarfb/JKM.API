using MediatR;
using System.Threading;
using JKM.PERSISTENCE.Utils;
using System.Threading.Tasks;
using JKM.PERSISTENCE.Repository.Proyecto;

namespace JKM.APPLICATION.Commands.Proyecto.UpdateActividadByProyecto
{
    public class UpdateActividadByProyectoCommandHandler : IRequestHandler<UpdateActividadByProyectoCommand, ResponseModel>
    {
        private readonly IProyectoRepository _proyectoRepository;

        public UpdateActividadByProyectoCommandHandler(IProyectoRepository proyectoRepository)
        {
            _proyectoRepository = proyectoRepository;
        }

        public async Task<ResponseModel> Handle(UpdateActividadByProyectoCommand request, CancellationToken cancellationToken)
        {
            ActividadProyectoModel model = new ActividadProyectoModel();
            model.UpdateActividad(descripcion: request.Descripcion, peso: request.Peso,
                idPadre: request.IdPadre, idHermano: request.IdHermano, idActividad: request.IdActividad,
                fechaInicio: request.FechaInicio, fechaFin: request.FechaFin);

            return await _proyectoRepository.UpdateActividadByProyecto(model);
        }
    }
}