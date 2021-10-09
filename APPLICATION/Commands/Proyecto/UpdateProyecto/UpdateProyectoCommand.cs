using FluentValidation;
using JKM.PERSISTENCE.Utils;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;
using System;

namespace JKM.APPLICATION.Commands.Proyecto.UpdateProyecto
{
    public class UpdateProyectoCommand : IRequest<ResponseModel>
    {
        [SwaggerSchema(ReadOnly = true)]
        public int IdProyecto { get; set; }
        public string NombreProyecto { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public string Descripcion { get; set; }
        public int IdEstado { get; set; }
        public decimal Precio { get; set; }
    }
    public class Validator : AbstractValidator<UpdateProyectoCommand>
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
