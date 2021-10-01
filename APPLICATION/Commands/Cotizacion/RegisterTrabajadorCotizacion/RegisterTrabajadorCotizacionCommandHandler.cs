using MediatR;
using System.Threading;
using System.Threading.Tasks;
using JKM.PERSISTENCE.Utils;
using JKM.PERSISTENCE.Repository.Cotizacion;
using FluentValidation.Results;
using FluentValidation;

namespace JKM.APPLICATION.Commands.Cotizacion.RegisterTrabajadorCotizacion
{
    public class RegisterTrabajadorCotizacionCommandHandler : IRequestHandler<RegisterTrabajadorCotizacionCommand, ResponseModel>
    {
        private readonly ICotizacionRepository _cotizacionRepository;

        public RegisterTrabajadorCotizacionCommandHandler(ICotizacionRepository cotizacionRepository)
        {
            _cotizacionRepository = cotizacionRepository;
        }

        public async Task<ResponseModel> Handle(RegisterTrabajadorCotizacionCommand request, CancellationToken cancellationToken)
        {
            ValidationResult validator = new Validator().Validate(request);
            Handlers.HandlerException(validator);

            TipoTrabajadorModel model = new TipoTrabajadorModel();
            model.RegisterTipoTrabajador(request.IdCotizacion, request.IdTipoTrabajador, request.Cantidad, request.Precio);

            return await _cotizacionRepository.RegisterTrabajadorCotizacion(model);
        }

        private class Validator : AbstractValidator<RegisterTrabajadorCotizacionCommand>
        {
            public Validator()
            {
                RuleFor(x => x.IdCotizacion)
                    .GreaterThan(0).WithMessage("El IdCotizacion debe ser un entero positivo");
                RuleFor(x => x.IdTipoTrabajador)
                    .GreaterThan(0).WithMessage("El idTipoTrabajador debe ser un entero positivo");
                RuleFor(x => x.Cantidad)
                    .GreaterThan(0).WithMessage("La cantidad de trabajadores debe ser un entero positivo");
                RuleFor(x => x.Precio)
                    .GreaterThan(0).WithMessage("El Precio debe ser un entero positivo");
            }
        }
    }
}
