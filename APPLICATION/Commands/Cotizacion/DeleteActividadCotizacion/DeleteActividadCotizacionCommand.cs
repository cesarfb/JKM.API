using MediatR;
using Swashbuckle.AspNetCore.Annotations;
using FluentValidation;
using JKM.UTILITY.Utils;

namespace JKM.APPLICATION.Commands.Cotizacion.DeleteActividadCotizacion
{
    public class DeleteActividadCotizacionCommand : IRequest<ResponseModel>
    {
        [SwaggerSchema(ReadOnly = true)]
        public int IdActividad { get; set; }
        [SwaggerSchema(ReadOnly = true)]
        public int IdCotizacion { get; set; }
    }
    public class Validator : AbstractValidator<DeleteActividadCotizacionCommand>
    {
        public Validator()
        {
            RuleFor(x => x.IdActividad)
                .GreaterThan(0).WithMessage("La IdActividad debe ser un entero positivo");
            RuleFor(x => x.IdCotizacion)
                .GreaterThan(0).WithMessage("La IdCotizacion debe ser un entero positivo");
        }
    }
}
