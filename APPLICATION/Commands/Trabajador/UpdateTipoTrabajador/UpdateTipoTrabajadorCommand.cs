using FluentValidation;
using JKM.UTILITY.Utils;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;

namespace JKM.APPLICATION.Commands.Trabajador.UpdateTipoTrabajador
{
    public class UpdateTipoTrabajadorCommand : IRequest<ResponseModel>
    {
        [SwaggerSchema(ReadOnly = true)]
        public int IdTipoTrabajador { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal PrecioReferencial { get; set; }
    }

    public class Validator : AbstractValidator<UpdateTipoTrabajadorCommand>
    {
        public Validator()
        {
            RuleFor(x => x.Nombre)
                .NotEmpty()
                .WithMessage("El nombre no puede ser vacio");
            RuleFor(x => x.Descripcion)
                .NotEmpty()
                .WithMessage("La descripcion paterno no puede ser vacio");
            RuleFor(x => x.PrecioReferencial)
                .NotEmpty()
                .GreaterThan(0)
                .WithMessage("El Precio debe ser un numero mayor a 0");
        }
    }
}
