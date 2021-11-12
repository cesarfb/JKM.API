using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JKM.APPLICATION.Commands.Notification.RecuperarUsuario
{
    public class RecuperarUsuarioNotificationCommand : INotification
    {
        public string Email { get; set; }
    }

    public class RecuperarUsuarioModel
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

    }

    public class Validator : AbstractValidator<RecuperarUsuarioNotificationCommand>
    {
        public Validator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("El correo no puede ser vacio")
                .EmailAddress().WithMessage("Ingrese un correo válido");
        }
    }
}
