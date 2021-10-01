using MediatR;
using JKM.PERSISTENCE.Utils;

namespace JKM.APPLICATION.Commands.Proyecto.DeleteTrabajadorByProyecto
{
    public class DeleteTrabajadorByProyectoCommand : IRequest<ResponseModel>
    {
        public int IdProyecto { get; set; }
        public int IdTrabajador{ get; set; }
    }
}
