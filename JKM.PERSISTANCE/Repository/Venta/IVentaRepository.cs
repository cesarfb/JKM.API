using JKM.UTILITY.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JKM.PERSISTENCE.Repository.Venta
{
    public interface IVentaRepository
    {
        Task<ResponseModel> RegisterVenta(VentaModel ventaModel);

        Task<ResponseModel> DeleteVenta(int idVenta);

    }
}
