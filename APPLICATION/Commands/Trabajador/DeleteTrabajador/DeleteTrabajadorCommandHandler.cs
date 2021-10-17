using JKM.PERSISTENCE.Repository.Trabajador;
using JKM.UTILITY.Utils;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace JKM.APPLICATION.Commands.Trabajador.DeleteTrabajador
{
    public class DeleteTrabajadorCommandHandler : IRequestHandler<DeleteTrabajadorCommand, ResponseModel>
    {
        private readonly ITrabajadorRepository _trabajadorRepository;
        public DeleteTrabajadorCommandHandler(ITrabajadorRepository trabajadorRepository)
        {
            _trabajadorRepository = trabajadorRepository;
        }
        public Task<ResponseModel> Handle(DeleteTrabajadorCommand request, CancellationToken cancellationToken)
        {
            return _trabajadorRepository.DeleteTrabajador(request.IdTrabajador);
        }
    }
}
