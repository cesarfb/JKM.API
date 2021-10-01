using System;
using MediatR;
using JKM.PERSISTENCE.Utils;
using Swashbuckle.AspNetCore.Annotations;

namespace JKM.APPLICATION.Commands.Proyecto.UpdateActividadByProyecto
{
    public class UpdateActividadByProyectoCommand : IRequest<ResponseModel>
    {
        [SwaggerSchema(ReadOnly = true)]
        public int IdProyecto { get; set; }
        [SwaggerSchema(ReadOnly = true)]
        public int IdActividad { get; set; }
        public string Descripcion { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public int Peso { get; set; }
        public int? IdPadre { get; set; }
        public int? IdHermano { get; set; }
    }
}
