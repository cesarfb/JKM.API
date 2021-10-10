using FluentValidation;
using JKM.UTILITY.Utils;
using MediatR;

namespace JKM.APPLICATION.Commands.Cotizacion.RechazarCotizacion
{
    public class RechazarCotizacionCommand : IRequest<ResponseModel>
    {
        public int IdCotizacion { get; set; }
    }
    public class Validator : AbstractValidator<RechazarCotizacionCommand>
    {
        public Validator()
        {
            RuleFor(x => x.IdCotizacion)
                .GreaterThan(0).WithMessage("El IdCotizacion debe ser un entero positivo");
        }
    }
}
