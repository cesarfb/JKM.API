using MediatR;
using System.Threading;
using JKM.PERSISTENCE.Utils;
using System.Threading.Tasks;
using JKM.PERSISTENCE.Repository.Cotizacion;
using FluentValidation;
using FluentValidation.Results;

namespace JKM.APPLICATION.Commands.Cotizacion.DeleteTrabajadorCotizacion
{
    public class DeleteTrabajadorCotizacionCommandHanlder : IRequestHandler<DeleteTrabajadorCotizacionCommand, ResponseModel>
    {
        private readonly ICotizacionRepository _cotizacionRepository;

        public DeleteTrabajadorCotizacionCommandHanlder(ICotizacionRepository cotizacionRepository)
        {
            _cotizacionRepository = cotizacionRepository;
        }

        public async Task<ResponseModel> Handle(DeleteTrabajadorCotizacionCommand request, CancellationToken cancellationToken)
        {
            ValidationResult validator = new Validator().Validate(request);
            Handlers.HandlerException(validator);

            return await _cotizacionRepository.DeleteTrabajadorCotizacion(request.IdCotizacion, request.IdTipoTrabajador);

        }
        private class Validator : AbstractValidator<DeleteTrabajadorCotizacionCommand>
        {
            public Validator()
            {
                RuleFor(x => x.IdCotizacion)
                    .GreaterThan(0).WithMessage("El IdCotizacion debe ser un entero positivo");
                RuleFor(x => x.IdTipoTrabajador)
                    .GreaterThan(0).WithMessage("El IdTipo debe ser un entero positivo");
            }
        }
    }
}
