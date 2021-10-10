using JKM.UTILITY.Utils;
using System.Threading.Tasks;

namespace JKM.PERSISTENCE.Repository.Proyecto
{
    public interface IProyectoRepository
    {
        Task<ResponseModel> RegisterProyecto(ProyectoModel proyectoModel);
        Task<ResponseModel> UpdateProyectoDetalle(ProyectoModel proyectoModel);
        Task<ResponseModel> RegisterTrabajadorProyecto(int proyectoId, int trabajadorId);
        Task<ResponseModel> DeleteTrabajadorByProyecto(int proyectoId, int trabajadorId);
        Task<ResponseModel> UpdateActividadByProyecto(ActividadProyectoModel actividadModel);
    }
}
