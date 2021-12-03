using JKM.UTILITY.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JKM.PERSISTENCE.Repository.Servicio
{
    public interface IServicioRepository
    {
        Task<ResponseModel> RegisterServicio(ServicioModel servicioModel);
        Task<ResponseModel> UpdateServicio(ServicioModel servicioModel);
        Task<ResponseModel> UpdateEstadoServicio(int idServicio);
    }
}
