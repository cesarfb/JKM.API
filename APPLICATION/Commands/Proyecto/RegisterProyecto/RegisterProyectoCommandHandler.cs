using MediatR;
using FluentValidation;
using System.Threading;
using JKM.PERSISTENCE.Utils;
using System.Threading.Tasks;
using FluentValidation.Results;
using JKM.PERSISTENCE.Repository.Proyecto;

namespace JKM.APPLICATION.Commands.Proyecto.RegisterProyecto
{
    public class RegisterProyectoCommandHandler : IRequestHandler<RegisterProyectoCommand, ResponseModel>
    {
        private readonly IProyectoRepository _proyectoRepository;
        public RegisterProyectoCommandHandler(IProyectoRepository proyectoRepository)
        {
            _proyectoRepository = proyectoRepository;
        }

        public async Task<ResponseModel> Handle(RegisterProyectoCommand request, CancellationToken cancellationToken)
        {
            ValidationResult validator = new Validator().Validate(request);
            Handlers.HandlerException(validator);

            ProyectoModel proyectoModel = new ProyectoModel();
            proyectoModel.RegisterProyecto(nombreProyecto: request.NombreProyecto,descripcion: request.Descripcion, 
                fechaInicio: request.FechaInicio,fechaFin: request.FechaFin);

            return await _proyectoRepository.RegisterProyecto(proyectoModel);
        }
        private class Validator : AbstractValidator<RegisterProyectoCommand>
        {
            public Validator()
            {
                RuleFor(x => x.NombreProyecto)
                    .NotEmpty().WithMessage("El nombre no puede ser vacio");
                RuleFor(x => x.Descripcion)
                       .NotEmpty().WithMessage("La descripcion no puede ser vacio");
                RuleFor(x => x.FechaFin)
                    .GreaterThanOrEqualTo(x => x.FechaInicio).WithMessage("La fecha fin debe ser mayor a la fecha de inicio");
            }
        }
    }
}
