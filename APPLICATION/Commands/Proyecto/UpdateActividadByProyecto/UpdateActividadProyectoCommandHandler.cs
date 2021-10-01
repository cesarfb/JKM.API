using MediatR;
using FluentValidation;
using System.Threading;
using JKM.PERSISTENCE.Utils;
using System.Threading.Tasks;
using FluentValidation.Results;
using JKM.PERSISTENCE.Repository.Proyecto;

namespace JKM.APPLICATION.Commands.Proyecto.UpdateActividadByProyecto
{
    public class UpdateActividadByProyectoCommandHandler : IRequestHandler<UpdateActividadByProyectoCommand, ResponseModel>
    {
        private readonly IProyectoRepository _proyectoRepository;

        public UpdateActividadByProyectoCommandHandler(IProyectoRepository proyectoRepository)
        {
            _proyectoRepository = proyectoRepository;
        }

        public async Task<ResponseModel> Handle(UpdateActividadByProyectoCommand request, CancellationToken cancellationToken)
        {
            ValidationResult validator = new Validator().Validate(request);
            Handlers.HandlerException(validator);

            ActividadProyectoModel model = new ActividadProyectoModel();
            model.UpdateActividad(descripcion: request.Descripcion, peso: request.Peso,
                idPadre: request.IdPadre, idHermano: request.IdHermano, idActividad: request.IdActividad,
                fechaInicio: request.FechaInicio, fechaFin: request.FechaFin);
            return await _proyectoRepository.UpdateActividadByProyecto(model);
        }

        private class Validator : AbstractValidator<UpdateActividadByProyectoCommand>
        {
            public Validator()
            {
                RuleFor(x => x.IdActividad)
                    .GreaterThan(0).WithMessage("El IdActividad debe ser un entero positivo");
                RuleFor(x => x.Descripcion)
                   .NotEmpty().WithMessage("La descripcion no puede ser vacío");
                RuleFor(x => x.Peso)
                    .GreaterThan(0).WithMessage("El peso debe ser un entero positivo");
            }
        }
    }
}