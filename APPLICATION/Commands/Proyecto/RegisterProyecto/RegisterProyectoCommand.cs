using MediatR;
using System;
using FluentValidation;
using JKM.UTILITY.Utils;

namespace JKM.APPLICATION.Commands.Proyecto.RegisterProyecto
{
    public class RegisterProyectoCommand : IRequest<ResponseModel>
    {
        public string NombreProyecto { get; set; }
        public DateTime FechaInicio{ get; set; }
        public DateTime FechaFin { get; set; }
        public string Descripcion { get; set; }
    }

    public class Validator : AbstractValidator<RegisterProyectoCommand>
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
