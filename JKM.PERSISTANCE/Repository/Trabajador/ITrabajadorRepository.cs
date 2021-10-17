using JKM.UTILITY.Utils;
using System.Threading.Tasks;

namespace JKM.PERSISTENCE.Repository.Trabajador
{
    public interface ITrabajadorRepository
    {
        Task<ResponseModel> RegisterTrabajador(TrabajadorModel model);
        Task<ResponseModel> UpdateTrabajador(TrabajadorModel model);
        Task<ResponseModel> DeleteTrabajador(int idTrabajador);
    }
}
