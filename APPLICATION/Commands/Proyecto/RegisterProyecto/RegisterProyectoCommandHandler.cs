using MediatR;
using System.Threading;
using System.Threading.Tasks;
using JKM.PERSISTENCE.Repository.Proyecto;
using JKM.UTILITY.Utils;

namespace JKM.APPLICATION.Commands.Proyecto.RegisterProyecto
{
    public class RegisterProyectoCommandHandler : IRequestHandler<RegisterProyectoCommand, ResponseModel>
    {
        private readonly IProyectoRepository _proyectoRepository;
        public RegisterProyectoCommandHandler(IProyectoRepository proyectoRepository)
        {
            _proyectoRepository = proyectoRepository;
        }

        public async Task<ResponseModel> Handle(RegisterProyectoCommand request, CancellationToken cancellationToken)
        {
            ProyectoModel proyectoModel = new ProyectoModel();
            proyectoModel.RegisterProyecto(nombreProyecto: request.NombreProyecto,descripcion: request.Descripcion, 
                fechaInicio: request.FechaInicio,fechaFin: request.FechaFin);

            return await _proyectoRepository.RegisterProyecto(proyectoModel);
        }
    }
}
