using MediatR;
using JKM.PERSISTENCE.Utils;
using Swashbuckle.AspNetCore.Annotations;

namespace JKM.APPLICATION.Commands.Cotizacion.DeleteActividadCotizacion
{
    public class DeleteActividadCotizacionCommand : IRequest<ResponseModel>
    {
        [SwaggerSchema(ReadOnly = true)]
        public int IdActividad { get; set; }
        [SwaggerSchema(ReadOnly = true)]
        public int IdCotizacion { get; set; }
    }
}
