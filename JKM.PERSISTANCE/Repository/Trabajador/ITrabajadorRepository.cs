using JKM.UTILITY.Utils;
using System.Threading.Tasks;

namespace JKM.PERSISTENCE.Repository.Trabajador
{
    public interface ITrabajadorRepository
    {
        Task<ResponseModel> RegisterTrabajador(TrabajadorModel model);
        Task<ResponseModel> UpdateTrabajador(TrabajadorModel model);
        Task<ResponseModel> DeleteTrabajador(int idTrabajador);
        Task<ResponseModel> RegisterTipoTrabajador(TipoTrabajadorProyectoModel model);
        Task<ResponseModel> UpdateTipoTrabajador(TipoTrabajadorProyectoModel model);
    }
}
