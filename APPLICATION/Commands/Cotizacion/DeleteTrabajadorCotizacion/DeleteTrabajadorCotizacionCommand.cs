using MediatR;
using JKM.PERSISTENCE.Utils;

namespace JKM.APPLICATION.Commands.Cotizacion.DeleteTrabajadorCotizacion
{
    public class DeleteTrabajadorCotizacionCommand : IRequest<ResponseModel>
    {
        public int IdTipoTrabajador{ get; set; }
        public int IdCotizacion { get; set; }
    }
}
