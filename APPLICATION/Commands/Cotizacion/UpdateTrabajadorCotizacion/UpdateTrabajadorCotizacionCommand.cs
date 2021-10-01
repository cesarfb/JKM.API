using MediatR;
using JKM.PERSISTENCE.Utils;
using Swashbuckle.AspNetCore.Annotations;

namespace JKM.APPLICATION.Commands.Cotizacion.UpdateTrabajadorCotizacion
{
    public class UpdateTrabajadorCotizacionCommand : IRequest<ResponseModel>
    {
        [SwaggerSchema(ReadOnly = true)]
        public int IdCotizacion { get; set; }
        [SwaggerSchema(ReadOnly = true)]
        public int IdTipoTrabajador { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }
    }
}
