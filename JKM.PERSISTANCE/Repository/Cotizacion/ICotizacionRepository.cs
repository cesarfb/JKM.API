using JKM.UTILITY.Utils;
using System.Threading.Tasks;

namespace JKM.PERSISTENCE.Repository.Cotizacion
{
    public interface ICotizacionRepository
    {
        Task<ResponseModel> AceptarCotizacion(int idCotizacion);
        Task<ResponseModel> RechazarCotizacion(int idCotizacion);
        Task<ResponseModel> RegisterCotizacion(CotizacionModel cotizacionModel);
        Task<ResponseModel> UpdateCotizacion(CotizacionModel cotizacionModel);
        Task<ResponseModel> RegisterTrabajadorCotizacion(TipoTrabajadorModel trabajadorModel);
        Task<ResponseModel> UpdateTrabajadorCotizacion(TipoTrabajadorModel trabajadorModel);
        Task<ResponseModel> DeleteTrabajadorCotizacion(int idCotizacion, int idTipo);
        Task<ResponseModel> RegisterActividadCotizacion(ActividadCotizacionModel actividadModel);
        Task<ResponseModel> UpdateActividadCotizacion(ActividadCotizacionModel actividadModel);
        Task<ResponseModel> DeleteActividadCotizacion(int idCotizacion, int idActividad);
    }
}
