using MediatR;
using JKM.PERSISTENCE.Utils;
using Swashbuckle.AspNetCore.Annotations;

namespace JKM.APPLICATION.Commands.Cotizacion.RegisterTrabajadorCotizacion
{
    public class RegisterTrabajadorCotizacionCommand : IRequest<ResponseModel>
    {
        [SwaggerSchema(ReadOnly = true)]
        public int IdCotizacion { get; set; }
        public int IdTipoTrabajador { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }
    }
}
