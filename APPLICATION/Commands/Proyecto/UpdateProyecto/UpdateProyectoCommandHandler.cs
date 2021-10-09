using MediatR;
using System.Threading;
using JKM.PERSISTENCE.Utils;
using System.Threading.Tasks;
using JKM.PERSISTENCE.Repository.Proyecto;

namespace JKM.APPLICATION.Commands.Proyecto.UpdateProyecto
{
    public class UpdateProyectoCommandHandler : IRequestHandler<UpdateProyectoCommand, ResponseModel>
    {
        private readonly IProyectoRepository _proyectoRepository;
        public UpdateProyectoCommandHandler(IProyectoRepository proyectoRepository)
        {
            _proyectoRepository = proyectoRepository;
        }

        public async Task<ResponseModel> Handle(UpdateProyectoCommand request, CancellationToken cancellationToken)
        {
            ProyectoModel proyectoModel = new ProyectoModel();
            proyectoModel.UpdateProyecto(idProyecto: request.IdProyecto, nombreProyecto: request.NombreProyecto,
                descripcion: request.Descripcion, fechaInicio: request.FechaInicio, fechaFin: request.FechaFin,
                idEstado: request.IdEstado, precio: request.Precio);

            return await _proyectoRepository.UpdateProyectoDetalle(proyectoModel);
        }
    }
}
