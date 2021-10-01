using MediatR;
using FluentValidation;
using System.Threading;
using JKM.PERSISTENCE.Utils;
using System.Threading.Tasks;
using FluentValidation.Results;
using JKM.PERSISTENCE.Repository.Cotizacion;

namespace JKM.APPLICATION.Commands.Cotizacion.UpdateTrabajadorCotizacion
{
    public class UpdateTrabajadorCotizacionCommandHandler : IRequestHandler<UpdateTrabajadorCotizacionCommand, ResponseModel>
    {
        private readonly ICotizacionRepository _cotizacionRepository;

        public UpdateTrabajadorCotizacionCommandHandler(ICotizacionRepository cotizacionRepository)
        {
            _cotizacionRepository = cotizacionRepository;
        }

        public async Task<ResponseModel> Handle(UpdateTrabajadorCotizacionCommand request, CancellationToken cancellationToken)
        {
            ValidationResult validator = new Validator().Validate(request);
            Handlers.HandlerException(validator);

            TipoTrabajadorModel model = new TipoTrabajadorModel();
            model.UpdateTipoTrabajador(request.IdCotizacion, request.IdTipoTrabajador, request.Cantidad, request.Precio);
            return await _cotizacionRepository.UpdateTrabajadorCotizacion(model);
        }

        private class Validator : AbstractValidator<UpdateTrabajadorCotizacionCommand>
        {
            public Validator()
            {
                RuleFor(x => x.IdCotizacion)
                    .GreaterThan(0).WithMessage("El IdCotizacion debe ser un entero positivo");
                RuleFor(x => x.IdTipoTrabajador)
                    .GreaterThan(0).WithMessage("El IdTipoTrabajador debe ser un entero positivo");
                RuleFor(x => x.Cantidad)
                    .GreaterThan(0).WithMessage("La cantidad de trabajadores debe ser un entero positivo");
                RuleFor(x => x.Precio)
                    .GreaterThan(0).WithMessage("El precio de trabajadores debe ser un entero positivo");
            }
        }
    }
}