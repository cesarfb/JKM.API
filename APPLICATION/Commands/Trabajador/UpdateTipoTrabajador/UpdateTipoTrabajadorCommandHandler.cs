using JKM.PERSISTENCE.Repository.Trabajador;
using JKM.UTILITY.Utils;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace JKM.APPLICATION.Commands.Trabajador.UpdateTipoTrabajador
{
    public class UpdateTipoTrabajadorCommandHandler : IRequestHandler<UpdateTipoTrabajadorCommand, ResponseModel>
    {
        private readonly ITrabajadorRepository _trabajadorRepository;
        public UpdateTipoTrabajadorCommandHandler(ITrabajadorRepository trabajadorRepository)
        {
            _trabajadorRepository = trabajadorRepository;
        }
        public Task<ResponseModel> Handle(UpdateTipoTrabajadorCommand request, CancellationToken cancellationToken)
        {
            TipoTrabajadorProyectoModel model = new TipoTrabajadorProyectoModel();
            model.Update(idTipoTrabajador: request.IdTipoTrabajador, nombre: request.Nombre, descripcion: request.Descripcion, precioReferencial: request.PrecioReferencial);
            return _trabajadorRepository.UpdateTipoTrabajador(model);
        }
    }
}
