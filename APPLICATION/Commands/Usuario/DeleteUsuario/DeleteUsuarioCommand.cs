using FluentValidation;
using JKM.UTILITY.Utils;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;

namespace JKM.APPLICATION.Commands.Usuario.DeleteUsuario
{
    public class DeleteUsuarioCommand : IRequest<ResponseModel>
    {
        [SwaggerSchema(ReadOnly = true)]
        public int IdUsuario { get; set; }
    }

    public class Validator : AbstractValidator<DeleteUsuarioCommand>
    {
        public Validator()
        {
            RuleFor(x => x.IdUsuario).GreaterThan(0).WithMessage("El IdUsuario debe ser un entero positivo");
        }
    }
}
