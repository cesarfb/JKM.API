using MediatR;
using System.Threading;
using JKM.PERSISTENCE.Utils;
using System.Threading.Tasks;
using JKM.PERSISTENCE.Repository.Cotizacion;
using FluentValidation.Results;
using FluentValidation;

namespace JKM.APPLICATION.Commands.Cotizacion.RegisterCotizacion
{
    public class RegisterCotizacionCommandHandler : IRequestHandler<RegisterCotizacionCommand, ResponseModel>
    {
        private readonly ICotizacionRepository _cotizacionRepository;

        public RegisterCotizacionCommandHandler(ICotizacionRepository cotizacionRepository)
        {
            _cotizacionRepository = cotizacionRepository;
        }

        public async Task<ResponseModel> Handle(RegisterCotizacionCommand request, CancellationToken cancellationToken)
        {
            ValidationResult validator = new Validator().Validate(request);
            Handlers.HandlerException(validator);

            CotizacionModel model = new CotizacionModel();
            model.RegisterCotizacion(solicitante: request.Solicitante, fechaSolicitud: request.FechaSolicitud, descripcion: request.Descripcion,
                email: request.Email, empresa: request.Empresa);
            return await _cotizacionRepository.RegisterCotizacion(model);
        }

        private class Validator : AbstractValidator<RegisterCotizacionCommand>
        {
            public Validator()
            {
                RuleFor(x => x.Solicitante)
                    .NotEmpty().WithMessage("El solicitante no puede ser vacío");
                RuleFor(x => x.Email)
                    .NotEmpty().WithMessage("El Email no puede ser vacío")
                    .EmailAddress().WithMessage("Formato incorrecto del correo");
                RuleFor(x => x.Empresa)
                    .NotEmpty().WithMessage("El solicitante no puede ser vacío");
            }
        }
    }
}
