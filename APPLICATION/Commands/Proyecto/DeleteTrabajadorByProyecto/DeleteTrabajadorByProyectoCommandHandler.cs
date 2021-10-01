using MediatR;
using System.Threading;
using FluentValidation;
using JKM.PERSISTENCE.Utils;
using System.Threading.Tasks;
using FluentValidation.Results;
using JKM.PERSISTENCE.Repository.Proyecto;

namespace JKM.APPLICATION.Commands.Proyecto.DeleteTrabajadorByProyecto
{
    public class DeleteTrabajadorByProyectoCommandHandler : IRequestHandler<DeleteTrabajadorByProyectoCommand, ResponseModel>
    {
        private readonly IProyectoRepository _proyectoRepository;

        public DeleteTrabajadorByProyectoCommandHandler(IProyectoRepository proyectoRepository)
        {
            _proyectoRepository = proyectoRepository;
        }

        public async Task<ResponseModel> Handle(DeleteTrabajadorByProyectoCommand request, CancellationToken cancellationToken)
        {
            ValidationResult validator = new Validator().Validate(request);
            Handlers.HandlerException(validator);

            return await _proyectoRepository.DeleteTrabajadorByProyecto(request.IdProyecto, request.IdTrabajador);
        }

        private class Validator : AbstractValidator<DeleteTrabajadorByProyectoCommand>
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
