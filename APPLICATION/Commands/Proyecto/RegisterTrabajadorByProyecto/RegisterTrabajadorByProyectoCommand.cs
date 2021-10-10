using MediatR;
using Swashbuckle.AspNetCore.Annotations;
using FluentValidation;
using JKM.UTILITY.Utils;

namespace JKM.APPLICATION.Commands.Proyecto.RegisterTrabajadorByProyecto
{
    public class RegisterTrabajadorByProyectoCommand : IRequest<ResponseModel>
    {
        [SwaggerSchema(ReadOnly = true)]
        public int IdProyecto { get; set; }
        public int IdTrabajador { get; set; }
    }

    public class Validator : AbstractValidator<RegisterTrabajadorByProyectoCommand>
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