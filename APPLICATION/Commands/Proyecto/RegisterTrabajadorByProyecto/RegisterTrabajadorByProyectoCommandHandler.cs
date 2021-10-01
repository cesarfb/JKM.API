using MediatR;
using FluentValidation;
using System.Threading;
using JKM.PERSISTENCE.Utils;
using System.Threading.Tasks;
using FluentValidation.Results;
using JKM.PERSISTENCE.Repository.Proyecto;

namespace JKM.APPLICATION.Commands.Proyecto.RegisterTrabajadorByProyecto
{
    public class RegisterTrabajadorByProyectoCommandHandler : IRequestHandler<RegisterTrabajadorByProyectoCommand, ResponseModel>
    {
        private readonly IProyectoRepository _proyectoRepository;
        public RegisterTrabajadorByProyectoCommandHandler(IProyectoRepository proyectoRepository)
        {
            _proyectoRepository = proyectoRepository;
        }

        public async Task<ResponseModel> Handle(RegisterTrabajadorByProyectoCommand request, CancellationToken cancellationToken)
        {
            ValidationResult validator = new Validator().Validate(request);
            Handlers.HandlerException(validator);

            return await _proyectoRepository.RegisterTrabajadorProyecto(request.IdProyecto, request.IdTrabajador);
        }

        private class Validator : AbstractValidator<RegisterTrabajadorByProyectoCommand>
        {
            public Validator()
            {
                RuleFor(x => x.IdTrabajador)
                    .GreaterThan(0).WithMessage("El IdTrabajador debe ser un entero positivo");
                RuleFor(x => x.IdProyecto)
                    .GreaterThan(0).WithMessage("El IdProyecto debe ser un entero positivo");
            }
        }
    }
}
