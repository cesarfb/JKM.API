using FluentValidation;
using JKM.UTILITY.Utils;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;
using System;

namespace JKM.APPLICATION.Commands.Trabajador.UpdateTrabajador
{
    public class UpdateTrabajadorCommand : IRequest<ResponseModel>
    {
        [SwaggerSchema(ReadOnly = true)]
        public int IdTrabajador { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string FechaNacimiento { get; set; }
        public int IdTipo { get; set; }
        public int IdEstado { get; set; }
    }

    public class Validator : AbstractValidator<UpdateTrabajadorCommand>
    {
        public Validator()
        {
            RuleFor(x => x.Nombre)
                .NotEmpty()
                .WithMessage("El nombre no puede ser vacio");
            RuleFor(x => x.ApellidoPaterno)
                .NotEmpty()
                .WithMessage("El apellido paterno no puede ser vacio");
            RuleFor(x => x.FechaNacimiento)
                .NotEmpty()
                .Must(date => Handlers.BeAValidDate(date, null, DateTime.Now.AddYears(-18)))
                .WithMessage($"La fecha de nacimiento debe ser anterior a {DateTime.Now.AddYears(-18):dd/MM/yyyy}");
            RuleFor(x => x.IdTipo)
                .GreaterThan(0)
                .WithMessage("El idTipo debe ser un numero entero positivo");
            RuleFor(x => x.IdEstado)
              .GreaterThan(0)
              .WithMessage("El idEstado debe ser un numero entero positivo");
        }
    }
}
