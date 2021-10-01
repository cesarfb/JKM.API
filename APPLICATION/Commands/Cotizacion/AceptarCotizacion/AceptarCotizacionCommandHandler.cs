using FluentValidation;
using FluentValidation.Results;
using JKM.PERSISTENCE.Repository.Cotizacion;
using JKM.PERSISTENCE.Utils;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace JKM.APPLICATION.Commands.Cotizacion.AceptarCotizacion
{
    public class AceptarCotizacionCommandHandler : IRequestHandler<AceptarCotizacionCommand, ResponseModel>
    {
        private readonly ICotizacionRepository _cotizacionRepository;

        public AceptarCotizacionCommandHandler(ICotizacionRepository cotizacionRepository)
        {
            _cotizacionRepository = cotizacionRepository;
        }

        public async Task<ResponseModel> Handle(AceptarCotizacionCommand request, CancellationToken cancellationToken)
        {
            ValidationResult validator = new Validator().Validate(request);
            Handlers.HandlerException(validator);

            return await _cotizacionRepository.AceptarCotizacion(request.IdCotizacion);
        }

        private class Validator : AbstractValidator<AceptarCotizacionCommand>
        {
            public Validator()
            {
                RuleFor(x => x.IdCotizacion)
                    .GreaterThan(0).WithMessage("El idCotizacion debe ser un entero positivo");
            }
        }
    }
}
