using MediatR;
using System.Threading;
using JKM.PERSISTENCE.Utils;
using System.Threading.Tasks;
using JKM.PERSISTENCE.Repository.Cotizacion;
using FluentValidation.Results;
using FluentValidation;

namespace JKM.APPLICATION.Commands.Cotizacion.UpdateActividadCotizacion
{
    public class UpdateActividadCotizacionCommandHandler : IRequestHandler<UpdateActividadCotizacionCommand, ResponseModel>
    {
        private readonly ICotizacionRepository _cotizacionRepository;

        public UpdateActividadCotizacionCommandHandler(ICotizacionRepository cotizacionRepository)
        {
            _cotizacionRepository = cotizacionRepository;
        }

        public async Task<ResponseModel> Handle(UpdateActividadCotizacionCommand request, CancellationToken cancellationToken)
        {
            ValidationResult validator = new Validator().Validate(request);
            Handlers.HandlerException(validator);

            ActividadCotizacionModel model = new ActividadCotizacionModel();
            model.UpdateActividad(descripcion: request.Descripcion, peso: request.Peso,
                idPadre: request.IdPadre, idHermano: request.IdHermano, idActividad: request.IdActividad);

            return await _cotizacionRepository.UpdateActividadCotizacion(model);
        }

        private class Validator : AbstractValidator<UpdateActividadCotizacionCommand>
        {
            public Validator()
            {
                RuleFor(x => x.Descripcion)
                    .NotEmpty().WithMessage("La descripcion no puede ser vacío");
                RuleFor(x => x.Peso)
                    .GreaterThan(0).WithMessage("El peso debe ser un entero positivo");
                RuleFor(x => x.IdActividad)
                    .GreaterThan(0).WithMessage("El IdActividad debe ser un entero positivo");
            }
        }
    }
}
