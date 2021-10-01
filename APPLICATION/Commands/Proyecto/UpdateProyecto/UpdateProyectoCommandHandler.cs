using MediatR;
using System.Threading;
using FluentValidation;
using JKM.PERSISTENCE.Utils;
using System.Threading.Tasks;
using FluentValidation.Results;
using JKM.PERSISTENCE.Repository.Proyecto;

namespace JKM.APPLICATION.Commands.Proyecto.UpdateProyecto
{
    public class UpdateProyectoCommandHandler : IRequestHandler<UpdateProyectoCommand, ResponseModel>
    {
        private readonly IProyectoRepository _proyectoRepository;
        public UpdateProyectoCommandHandler(IProyectoRepository proyectoRepository)
        {
            _proyectoRepository = proyectoRepository;
        }

        public async Task<ResponseModel> Handle(UpdateProyectoCommand request, CancellationToken cancellationToken)
        {
            ValidationResult validator = new Validator().Validate(request);
            Handlers.HandlerException(validator);

            ProyectoModel proyectoModel = new ProyectoModel();
            proyectoModel.UpdateProyecto(idProyecto: request.IdProyecto, nombreProyecto: request.NombreProyecto,
                descripcion: request.Descripcion, fechaInicio: request.FechaInicio, fechaFin: request.FechaFin,
                idEstado: request.IdEstado, precio: request.Precio);
            return await _proyectoRepository.UpdateProyectoDetalle(proyectoModel);
        }

        private class Validator : AbstractValidator<UpdateProyectoCommand>
        {
            public Validator()
            {
                RuleFor(x => x.IdProyecto)
                    .GreaterThan(0).WithMessage("El IdProyecto debe ser un entero positivo");
                RuleFor(x => x.NombreProyecto)
                    .NotEmpty().WithMessage("El nombre no puede ser vacio");
                RuleFor(x => x.Precio)
                    .GreaterThan(0).WithMessage("El precio debe ser un entero positivo");
                RuleFor(x => x.IdEstado)
                    .GreaterThan(0).WithMessage("El IdEstado debe ser un entero positivo");
            }
        }
    }
}
