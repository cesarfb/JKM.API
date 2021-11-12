using JKM.APPLICATION.Aggregates;
using JKM.UTILITY.Utils;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JKM.APPLICATION.Queries.Usuario.GetUsuarioPaginado
{
    public class GetUsuarioPaginadoQuery : IRequest<PaginadoResponse<UsuarioModel>>
    {
    }
}
