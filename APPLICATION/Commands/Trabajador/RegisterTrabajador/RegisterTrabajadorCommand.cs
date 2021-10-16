using FluentValidation;
using JKM.UTILITY.Utils;
using MediatR;
using System;

namespace JKM.APPLICATION.Commands.Trabajador.RegisterTrabajador
{
    public class RegisterTrabajadorCommand : IRequest<ResponseModel>
    {
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string FechaNacimiento { get; set; }
        public int IdTipo { get; set; }
    }

    public class Validator : AbstractValidator<RegisterTrabajadorCommand>
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
        }
    }
}
