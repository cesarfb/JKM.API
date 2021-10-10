using MediatR;
using System.Threading;
using System.Threading.Tasks;
using JKM.PERSISTENCE.Repository.Proyecto;
using JKM.UTILITY.Utils;

namespace JKM.APPLICATION.Commands.Proyecto.RegisterTrabajadorByProyecto
{
    public class RegisterTrabajadorByProyectoCommandHandler : IRequestHandler<RegisterTrabajadorByProyectoCommand, ResponseModel>
    {
        private readonly IProyectoRepository _proyectoRepository;
        public RegisterTrabajadorByProyectoCommandHandler(IProyectoRepository proyectoRepository)
        {
            _proyectoRepository = proyectoRepository;
        }

        public async Task<ResponseModel> Handle(RegisterTrabajadorByProyectoCommand request, CancellationToken cancellationToken)
        {
            return await _proyectoRepository.RegisterTrabajadorProyecto(request.IdProyecto, request.IdTrabajador);
        }
    }
}
