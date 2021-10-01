using MediatR;
using FluentValidation;
using System.Threading;
using JKM.PERSISTENCE.Utils;
using System.Threading.Tasks;
using FluentValidation.Results;
using JKM.PERSISTENCE.Repository.Cotizacion;

namespace JKM.APPLICATION.Commands.Cotizacion.RechazarCotizacion
{
    public class RechazarCotizacionCommandHandler : IRequestHandler<RechazarCotizacionCommand, ResponseModel>
    {
        private readonly ICotizacionRepository _cotizacionRepository;

        public RechazarCotizacionCommandHandler(ICotizacionRepository cotizacionRepository)
        {
            _cotizacionRepository = cotizacionRepository;
        }
        
        public async Task<ResponseModel> Handle(RechazarCotizacionCommand request, CancellationToken cancellationToken)
        {
            ValidationResult validator = new Validator().Validate(request);
            Handlers.HandlerException(validator);

            return await _cotizacionRepository.RechazarCotizacion(request.IdCotizacion);
        }

        private class Validator : AbstractValidator<RechazarCotizacionCommand>
        {
            public Validator()
            {
                RuleFor(x => x.IdCotizacion)
                    .GreaterThan(0).WithMessage("El IdCotizacion debe ser un entero positivo");
            }
        }
    }
}
