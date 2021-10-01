using MediatR;
using System.Threading;
using JKM.PERSISTENCE.Utils;
using System.Threading.Tasks;
using JKM.PERSISTENCE.Repository.Cotizacion;
using FluentValidation.Results;
using FluentValidation;

namespace JKM.APPLICATION.Commands.Cotizacion.RegisterActividadCotizacion
{
    public class RegisterActividadCotizacionCommandHandler : IRequestHandler<RegisterActividadCotizacionCommand, ResponseModel>
    {
        private readonly ICotizacionRepository _cotizacionRepository;

        public RegisterActividadCotizacionCommandHandler(ICotizacionRepository cotizacionRepository)
        {
            _cotizacionRepository = cotizacionRepository;
        }

        public async Task<ResponseModel> Handle(RegisterActividadCotizacionCommand request, CancellationToken cancellationToken)
        {
            ValidationResult validator = new Validator().Validate(request);
            Handlers.HandlerException(validator);

            ActividadCotizacionModel model = new ActividadCotizacionModel();
            model.RegisterActividad(descripcion: request.Descripcion, peso: request.Peso,
                idPadre: request.IdPadre, idHermano: request.IdHermano, idCotizacion: request.IdCotizacion);

            return await _cotizacionRepository.RegisterActividadCotizacion(model);
        }

        private class Validator : AbstractValidator<RegisterActividadCotizacionCommand>
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
}
