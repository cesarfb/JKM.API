using JKM.UTILITY.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JKM.PERSISTENCE.Repository.Usuario
{
    public interface IUsuarioRepository
    {
        Task<ResponseModel> RegisterUsuario(UsuarioModel usuarioModel);
        Task<ResponseModel> UpdateUsuario(UsuarioModel usuarioModel);
        Task<ResponseModel> UpdateEstado(int idUsuario);
        Task<ResponseModel> DeleteUsuario(int idDetalleUsuario);

    }
}
