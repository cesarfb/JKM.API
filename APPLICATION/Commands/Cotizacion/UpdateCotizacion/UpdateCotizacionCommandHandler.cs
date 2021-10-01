using MediatR;
using System.Threading;
using FluentValidation;
using JKM.PERSISTENCE.Utils;
using System.Threading.Tasks;
using FluentValidation.Results;
using JKM.PERSISTENCE.Repository.Cotizacion;

namespace JKM.APPLICATION.Commands.Cotizacion.UpdateCotizacion
{
    public class UpdateCotizacionCommandHandler : IRequestHandler<UpdateCotizacionCommand, ResponseModel>
    {
        private readonly ICotizacionRepository _cotizacionRepository;

        public UpdateCotizacionCommandHandler(ICotizacionRepository cotizacionRepository)
        {
            _cotizacionRepository = cotizacionRepository;
        }

        public async Task<ResponseModel> Handle(UpdateCotizacionCommand request, CancellationToken cancellationToken)
        {
            ValidationResult validator = new Validator().Validate(request);
            Handlers.HandlerException(validator);

            CotizacionModel cotizacion = new CotizacionModel();
            cotizacion.UpdateCotizacion(solicitante: request.Solicitante, fechaSolicitud: request.FechaSolicitud, descripcion: request.Descripcion, 
                email: request.Email,empresa: request.Empresa, idCotizacion: request.IdCotizacion, idEstado: request.IdEstado, 
                request.IdPrecioCotizacion, request.PrecioCotizacion);

            return await _cotizacionRepository.UpdateCotizacion(cotizacion);
        }

        private class Validator : AbstractValidator<UpdateCotizacionCommand>
        {
            public Validator()
            {
                RuleFor(x => x.IdCotizacion)
                    .GreaterThan(0).WithMessage("El IdCotizacion debe ser un entero positivo");
                RuleFor(x => x.Solicitante)
                    .NotEmpty().WithMessage("El solicitante no puede ser vacío");
                RuleFor(x => x.Email)
                   .NotEmpty().WithMessage("El email no puede ser vacío")
                   .EmailAddress().WithMessage("Formato erroneo del correo");
                RuleFor(x => x.Empresa)
                    .NotEmpty().WithMessage("La empresa no puede estar vacía");
                RuleFor(x => x.IdEstado)
                    .GreaterThan(0).WithMessage("El idEstado debe ser un entero positivo");
            }
        }
    }
}
