using JKM.PERSISTENCE.Repository.Usuario;
using JKM.UTILITY.Utils;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JKM.APPLICATION.Commands.Usuario.RegisterUsuario
{
    public class RegisterUsuarioCommandHandler : IRequestHandler<RegisterUsuarioCommand, ResponseModel>
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public RegisterUsuarioCommandHandler(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<ResponseModel> Handle(RegisterUsuarioCommand request, CancellationToken cancellationToken)
        {
            UsuarioModel usuarioModel = new UsuarioModel();

            usuarioModel.RegisterUsuario(nombre: request.Nombre, apellido: request.Apellido, email: request.Email, fechaNacimiento: request.FechaNacimiento, username: request.Username, password: request.Password, idRol: request.IdRol);

            return await _usuarioRepository.RegisterUsuario(usuarioModel);
        }
    }
}
