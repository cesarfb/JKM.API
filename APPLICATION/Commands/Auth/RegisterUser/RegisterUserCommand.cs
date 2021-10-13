using FluentValidation;
using JKM.UTILITY.Utils;
using MediatR;
using System;

namespace JKM.APPLICATION.Commands.Auth.RegisterUser
{
    public class RegisterUserCommand : IRequest<ResponseModel>
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string FechaNacimiento { get; set; }
    }

    public class Validator : AbstractValidator<RegisterUserCommand>
    {
        public Validator()
        {
            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("El usuario no puede estar vacío.");
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("La contraseña no puede estar vacío.");
            RuleFor(x => x.Nombre)
                .NotEmpty().WithMessage("El nombre no puede estar vacío.");
            RuleFor(x => x.Apellido)
                .NotEmpty().WithMessage("El apellido no puede estar vacío.");
            RuleFor(x => x.FechaNacimiento)
                .NotEmpty()
                .Must(date => Handlers.BeAValidDate(date, null, DateTime.Now.AddYears(-18)))
                .WithMessage($"La fecha de nacimiento debe ser anterior a {DateTime.Now.AddYears(-18):dd/MM/yyyy}");
        }
    }
}
