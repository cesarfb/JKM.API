using JKM.PERSISTENCE.Repository.Trabajador;
using JKM.UTILITY.Utils;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace JKM.APPLICATION.Commands.Trabajador.UpdateTrabajador
{
    public class UpdateTrabajadorCommandHandler : IRequestHandler<UpdateTrabajadorCommand, ResponseModel>
    {
        private readonly ITrabajadorRepository _trabajadorRepository;
        public UpdateTrabajadorCommandHandler(ITrabajadorRepository trabajadorRepository)
        {
            _trabajadorRepository = trabajadorRepository;
        }
        public Task<ResponseModel> Handle(UpdateTrabajadorCommand request, CancellationToken cancellationToken)
        {
            TrabajadorModel model = new TrabajadorModel();
            model.Update(idTrabajador: request.IdTrabajador, nombre: request.Nombre, apellidoPaterno: request.ApellidoPaterno, 
                apellidoMaterno: request.ApellidoMaterno, fechaNacimiento: request.FechaNacimiento, idTipo: request.IdTipo, idEstado: request.IdEstado);
            return _trabajadorRepository.UpdateTrabajador(model);
        }
    }
}
