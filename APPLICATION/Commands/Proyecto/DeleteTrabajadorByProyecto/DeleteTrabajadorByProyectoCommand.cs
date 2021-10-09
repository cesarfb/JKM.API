using MediatR;
using JKM.PERSISTENCE.Utils;
using FluentValidation;

namespace JKM.APPLICATION.Commands.Proyecto.DeleteTrabajadorByProyecto
{
    public class DeleteTrabajadorByProyectoCommand : IRequest<ResponseModel>
    {
        public int IdProyecto { get; set; }
        public int IdTrabajador{ get; set; }
    }

    public class Validator : AbstractValidator<DeleteTrabajadorByProyectoCommand>
    {
        public Validator()
        {
            RuleFor(x => x.IdTrabajador)
                .GreaterThan(0).WithMessage("El IdTrabajador debe ser un entero positivo");
            RuleFor(x => x.IdProyecto)
                .GreaterThan(0).WithMessage("El IdProyecto debe ser un entero positivo");
        }
    }
}
