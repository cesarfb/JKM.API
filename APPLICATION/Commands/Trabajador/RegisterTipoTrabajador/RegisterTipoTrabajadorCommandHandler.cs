using JKM.PERSISTENCE.Repository.Trabajador;
using JKM.UTILITY.Utils;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace JKM.APPLICATION.Commands.Trabajador.RegisterTipoTrabajador
{
    public class RegisterTipoTrabajadorCommandHandler : IRequestHandler<RegisterTipoTrabajadorCommand, ResponseModel>
    {
        private readonly ITrabajadorRepository _trabajadorRepository;
        public RegisterTipoTrabajadorCommandHandler(ITrabajadorRepository trabajadorRepository)
        {
            _trabajadorRepository = trabajadorRepository;
        }
        public Task<ResponseModel> Handle(RegisterTipoTrabajadorCommand request, CancellationToken cancellationToken)
        {
            TipoTrabajadorProyectoModel model = new TipoTrabajadorProyectoModel();
            model.Register(nombre: request.Nombre, descripcion: request.Descripcion, precioReferencial: request.PrecioReferencial);
            return _trabajadorRepository.RegisterTipoTrabajador(model);
        }
    }
}
