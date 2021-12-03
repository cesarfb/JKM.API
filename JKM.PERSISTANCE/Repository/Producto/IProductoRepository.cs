using JKM.UTILITY.Utils;
using System.Threading.Tasks;

namespace JKM.PERSISTENCE.Repository.Producto
{
    public interface IProductoRepository
    {
        Task<ResponseModel> RegisterProducto(ProductoModel model);
        Task<ResponseModel> UpdateProducto(ProductoModel model);
    }
}
