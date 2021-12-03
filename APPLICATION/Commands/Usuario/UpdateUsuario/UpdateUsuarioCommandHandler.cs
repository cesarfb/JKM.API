using JKM.PERSISTENCE.Repository.Usuario;
using JKM.UTILITY.Utils;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JKM.APPLICATION.Commands.Usuario.UpdateUsuario
{
    public class UpdateUsuarioCommandHandler : IRequestHandler<UpdateUsuarioCommand, ResponseModel>
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UpdateUsuarioCommandHandler(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<ResponseModel> Handle(UpdateUsuarioCommand request, CancellationToken cancellationToken)
        {
            UsuarioModel usuarioModel = new UsuarioModel();

            usuarioModel.UpdateUsuario(idUsuario: request.IdUsuario, nombre: request.Nombre, apellido: request.Apellido, email: request.Email, fechaNacimiento: request.FechaNacimiento, username: request.Username, password: request.Password, idRol: request.IdRol);

            return await _usuarioRepository.UpdateUsuario(usuarioModel);
        }
    }
}
