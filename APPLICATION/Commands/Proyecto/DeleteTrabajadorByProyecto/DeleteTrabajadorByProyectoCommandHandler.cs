using MediatR;
using System.Threading;
using System.Threading.Tasks;
using JKM.PERSISTENCE.Repository.Proyecto;
using JKM.UTILITY.Utils;

namespace JKM.APPLICATION.Commands.Proyecto.DeleteTrabajadorByProyecto
{
    public class DeleteTrabajadorByProyectoCommandHandler : IRequestHandler<DeleteTrabajadorByProyectoCommand, ResponseModel>
    {
        private readonly IProyectoRepository _proyectoRepository;

        public DeleteTrabajadorByProyectoCommandHandler(IProyectoRepository proyectoRepository)
        {
            _proyectoRepository = proyectoRepository;
        }

        public async Task<ResponseModel> Handle(DeleteTrabajadorByProyectoCommand request, CancellationToken cancellationToken)
        {
            return await _proyectoRepository.DeleteTrabajadorByProyecto(request.IdProyecto, request.IdTrabajador);
        }
    }
}
