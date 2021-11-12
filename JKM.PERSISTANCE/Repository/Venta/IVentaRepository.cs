using JKM.PERSISTENCE.Repository.Proyecto;
using JKM.UTILITY.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JKM.PERSISTENCE.Repository.Venta
{
    public interface IVentaRepository
    {
        Task<ResponseModel> RegisterVenta(VentaModel ventaModel, ProyectoModel proyectoModel);

        Task<ResponseModel> DeleteVenta(int idVenta);

    }
}
