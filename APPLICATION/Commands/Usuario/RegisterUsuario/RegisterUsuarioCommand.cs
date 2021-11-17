using FluentValidation;
using JKM.UTILITY.Utils;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JKM.APPLICATION.Commands.Usuario.RegisterUsuario
{
    public class RegisterUsuarioCommand : IRequest<ResponseModel>
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int IdRol { get; set; }
    }

    public class Validator : AbstractValidator<RegisterUsuarioCommand>
    {
        public Validator()
        {
            RuleFor(x => x.Nombre).NotEmpty().WithMessage("El nombre no puede ser vacio");
            RuleFor(x => x.Apellido).NotEmpty().WithMessage("El apellido no puede ser vacio");
            RuleFor(x => x.Email).NotEmpty().WithMessage("El email no puede ser vacio");
            RuleFor(x => x.FechaNacimiento).NotEmpty().WithMessage("La fecha de nacimiento no puede ser vacio");
            RuleFor(x => x.Username).NotEmpty().WithMessage("El nombre de usuario no puede ser vacio");
            RuleFor(x => x.Password).NotEmpty().WithMessage("La contraseña no puede ser vacia");
            RuleFor(x => x.IdRol).NotEmpty().WithMessage("El rol no puede ser vacio");
        }
    }
}
