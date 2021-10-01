using MediatR;
using JKM.PERSISTENCE.Utils;
using Swashbuckle.AspNetCore.Annotations;

namespace JKM.APPLICATION.Commands.Proyecto.RegisterTrabajadorByProyecto
{
    public class RegisterTrabajadorByProyectoCommand : IRequest<ResponseModel>
    {
        [SwaggerSchema(ReadOnly = true)]
        public int IdProyecto { get; set; }
        public int IdTrabajador { get; set; }
    }
}
