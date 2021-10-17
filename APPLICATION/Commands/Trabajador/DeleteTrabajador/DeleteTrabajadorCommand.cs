using FluentValidation;
using JKM.UTILITY.Utils;
using MediatR;

namespace JKM.APPLICATION.Commands.Trabajador.DeleteTrabajador
{
    public class DeleteTrabajadorCommand : IRequest<ResponseModel>
    {
        public int IdTrabajador { get; set; }
    }
    public class Validator : AbstractValidator<DeleteTrabajadorCommand>
    {
        public Validator()
        {
            RuleFor(x => x.IdTrabajador)
                .GreaterThan(0)
                .WithMessage("El idTrabajador debe ser un numero entero positivo");
        }
    }
}
