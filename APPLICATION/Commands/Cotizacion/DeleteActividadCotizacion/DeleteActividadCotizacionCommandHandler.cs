using MediatR;
using System.Threading;
using JKM.PERSISTENCE.Utils;
using System.Threading.Tasks;
using JKM.PERSISTENCE.Repository.Cotizacion;
using FluentValidation;
using FluentValidation.Results;

namespace JKM.APPLICATION.Commands.Cotizacion.DeleteActividadCotizacion
{
    public class DeleteActividadCotizacionCommandHandler : IRequestHandler<DeleteActividadCotizacionCommand, ResponseModel>
    {
        private readonly ICotizacionRepository _cotizacionRepository;

        public DeleteActividadCotizacionCommandHandler(ICotizacionRepository cotizacionRepository)
        {
            _cotizacionRepository = cotizacionRepository;
        }

        public async Task<ResponseModel> Handle(DeleteActividadCotizacionCommand request, CancellationToken cancellationToken)
        {
            ValidationResult validator = new Validator().Validate(request);
            Handlers.HandlerException(validator);

            return await _cotizacionRepository.DeleteActividadCotizacion(request.IdCotizacion, request.IdActividad);
        }

        private class Validator : AbstractValidator<DeleteActividadCotizacionCommand>
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
}
