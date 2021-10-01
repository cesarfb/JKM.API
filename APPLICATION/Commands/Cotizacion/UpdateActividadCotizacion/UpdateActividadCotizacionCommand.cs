using System;
using MediatR;
using JKM.PERSISTENCE.Utils;
using Swashbuckle.AspNetCore.Annotations;

namespace JKM.APPLICATION.Commands.Cotizacion.UpdateActividadCotizacion
{
    public class UpdateActividadCotizacionCommand : IRequest<ResponseModel>
    {
        [SwaggerSchema(ReadOnly = true)]
        public int IdActividad { get; set; }
        [SwaggerSchema(ReadOnly = true)]
        public int IdCotizacion { get; set; }
        public string Descripcion { get; set; }
        public int Peso { get; set; }
        public int? IdPadre { get; set; }
        public int? IdHermano { get; set; }
    }
}
