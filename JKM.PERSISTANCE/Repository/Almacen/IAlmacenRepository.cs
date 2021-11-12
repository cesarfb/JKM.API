using JKM.UTILITY.Utils;
using System.Threading.Tasks;

namespace JKM.PERSISTENCE.Repository.Almacen
{
    public interface IAlmacenRepository
    {
        Task<ResponseModel> RegisterAlmacen(AlmacenModel model);
        Task<ResponseModel> UpdateAlmacen(AlmacenModel model);
    }
}
