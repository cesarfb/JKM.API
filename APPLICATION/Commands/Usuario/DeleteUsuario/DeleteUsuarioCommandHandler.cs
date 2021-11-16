using JKM.PERSISTENCE.Repository.Usuario;
using JKM.UTILITY.Utils;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JKM.APPLICATION.Commands.Usuario.DeleteUsuario
{
    public class DeleteUsuarioCommandHandler : IRequestHandler<DeleteUsuarioCommand, ResponseModel>
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public DeleteUsuarioCommandHandler(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<ResponseModel> Handle(DeleteUsuarioCommand request, CancellationToken cancellationToken)
        {
            return await _usuarioRepository.DeleteUsuario(request.IdUsuario);
        }
    }
}
