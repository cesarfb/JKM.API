using MediatR;
using Swashbuckle.AspNetCore.Annotations;
using FluentValidation;
using JKM.UTILITY.Utils;

namespace JKM.APPLICATION.Commands.Cotizacion.RegisterActividadCotizacion
{
    public class RegisterActividadCotizacionCommand : IRequest<ResponseModel>
    {
        [SwaggerSchema(ReadOnly = true)]
        public int IdCotizacion { get; set; }
        public string Descripcion { get; set; }
        public int Peso { get; set; }
        public int? IdPadre { get; set; }
        public int? IdHermano  { get; set; }
    }
    public class Validator : AbstractValidator<RegisterActividadCotizacionCommand>
    {
        public Validator()
        {
            RuleFor(x => x.Descripcion)
                .NotEmpty().WithMessage("La descripcion no puede ser vacío");
            RuleFor(x => x.Peso)
                .GreaterThan(0).WithMessage("El peso debe ser un entero positivo");
            RuleFor(x => x.IdCotizacion)
                .GreaterThan(0).WithMessage("El IdCotizacion debe ser un entero positivo");
        }
    }
}
