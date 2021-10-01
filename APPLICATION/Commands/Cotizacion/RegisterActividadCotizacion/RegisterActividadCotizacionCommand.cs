using System;
using MediatR;
using JKM.PERSISTENCE.Utils;
using Swashbuckle.AspNetCore.Annotations;

namespace JKM.APPLICATION.Commands.Cotizacion.RegisterActividadCotizacion
{
    public class RegisterActividadCotizacionCommand : IRequest<ResponseModel>
    {
        [SwaggerSchema(ReadOnly = true)]
        public int IdCotizacion { get; set; }
        public string Descripcion { get; set; }
        public int Peso { get; set; }
        public int? IdPadre { get; set; }
        public int? IdHermano  { get; set; }
    }
}
