using JKM.PERSISTENCE.Utils;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;
using System;

namespace JKM.APPLICATION.Commands.Cotizacion.UpdateCotizacion
{
    public class UpdateCotizacionCommand : IRequest<ResponseModel>
    {
        [SwaggerSchema(ReadOnly = true)]
        public int IdCotizacion { get; set; }
        public string Solicitante { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public string Email { get; set; }
        public string Empresa { get; set; }
        public int IdEstado { get; set; }
        public int IdPrecioCotizacion { get; set; }
        public double PrecioCotizacion { get; set; }
    }
}
