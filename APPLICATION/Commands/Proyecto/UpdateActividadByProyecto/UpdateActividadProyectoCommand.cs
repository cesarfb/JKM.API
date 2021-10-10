using System;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;
using FluentValidation;
using JKM.UTILITY.Utils;

namespace JKM.APPLICATION.Commands.Proyecto.UpdateActividadByProyecto
{
    public class UpdateActividadByProyectoCommand : IRequest<ResponseModel>
    {
        [SwaggerSchema(ReadOnly = true)]
        public int IdProyecto { get; set; }
        [SwaggerSchema(ReadOnly = true)]
        public int IdActividad { get; set; }
        public string Descripcion { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public int Peso { get; set; }
        public int? IdPadre { get; set; }
        public int? IdHermano { get; set; }
    }

    public class Validator : AbstractValidator<UpdateActividadByProyectoCommand>
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
