using JKM.PERSISTENCE.Repository.Trabajador;
using JKM.UTILITY.Utils;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace JKM.APPLICATION.Commands.Trabajador.RegisterTrabajador
{
    public class RegisterTrabajadorCommandHandler : IRequestHandler<RegisterTrabajadorCommand, ResponseModel>
    {
        private readonly ITrabajadorRepository _trabajadorRepository;
        public RegisterTrabajadorCommandHandler(ITrabajadorRepository trabajadorRepository)
        {
            _trabajadorRepository = trabajadorRepository;
        }
        public Task<ResponseModel> Handle(RegisterTrabajadorCommand request, CancellationToken cancellationToken)
        {
            TrabajadorModel model = new TrabajadorModel();
            model.Register(nombre: request.Nombre, apellidoPaterno: request.ApellidoPaterno, apellidoMaterno: request.ApellidoMaterno,
                fechaNacimiento: request.FechaNacimiento, idTipo: request.IdTipo);
            return _trabajadorRepository.RegisterTrabajador(model);
        }
    }
}
