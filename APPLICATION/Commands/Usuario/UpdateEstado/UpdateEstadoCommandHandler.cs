﻿using JKM.PERSISTENCE.Repository.Usuario;
using JKM.UTILITY.Utils;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JKM.APPLICATION.Commands.Usuario.UpdateEstado
{
    public class UpdateEstadoCommandHandler : IRequestHandler<UpdateEstadoCommand, ResponseModel>
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UpdateEstadoCommandHandler(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<ResponseModel> Handle(UpdateEstadoCommand request, CancellationToken cancellationToken)
        {
            return await _usuarioRepository.UpdateEstado(request.IdUsuario);
        }
    }
}
