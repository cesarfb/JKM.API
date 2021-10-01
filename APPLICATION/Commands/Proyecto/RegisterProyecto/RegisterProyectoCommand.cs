using MediatR;
using JKM.PERSISTENCE.Utils;
using System;

namespace JKM.APPLICATION.Commands.Proyecto.RegisterProyecto
{
    public class RegisterProyectoCommand : IRequest<ResponseModel>
    {
        public string NombreProyecto { get; set; }
        public DateTime FechaInicio{ get; set; }
        public DateTime FechaFin { get; set; }
        public string Descripcion { get; set; }
    }
}
