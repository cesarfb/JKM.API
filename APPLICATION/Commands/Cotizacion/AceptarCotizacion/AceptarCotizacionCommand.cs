using FluentValidation;
using JKM.UTILITY.Utils;
using MediatR;

namespace JKM.APPLICATION.Commands.Cotizacion.AceptarCotizacion
{
    public class AceptarCotizacionCommand : IRequest<ResponseModel>
    {
        public int IdCotizacion { get; set; }
        public string Descripcion { get; set; }
        public string Nombre { get; set; }
    }
    public class Validator : AbstractValidator<AceptarCotizacionCommand>
    {
        public Validator()
        {
            RuleFor(x => x.IdCotizacion)
                .GreaterThan(0).WithMessage("El idCotizacion debe ser un entero positivo");

            RuleFor(x => x.Nombre.Trim())
                .NotEmpty().WithMessage("El proyecto debe tener un nombre");

            RuleFor(x => x.Descripcion.Trim())
                .NotEmpty().WithMessage("El proyecto debe tener una descripción");
        }
    }
}
