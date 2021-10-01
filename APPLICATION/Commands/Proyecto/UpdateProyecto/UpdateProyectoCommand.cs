using JKM.PERSISTENCE.Utils;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;
using System;

namespace JKM.APPLICATION.Commands.Proyecto.UpdateProyecto
{
    public class UpdateProyectoCommand : IRequest<ResponseModel>
    {
        [SwaggerSchema(ReadOnly = true)]
        public int IdProyecto { get; set; }
        public string NombreProyecto { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public string Descripcion { get; set; }
        public int IdEstado { get; set; }
        public decimal Precio { get; set; }
    }
}
