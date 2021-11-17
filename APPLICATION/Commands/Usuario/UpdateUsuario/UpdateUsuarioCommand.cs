using FluentValidation;
using JKM.UTILITY.Utils;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JKM.APPLICATION.Commands.Usuario.UpdateUsuario
{
    public class UpdateUsuarioCommand : IRequest<ResponseModel>
    {
        public int IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int IdRol { get; set; }
    }

    public class Validator : AbstractValidator<UpdateUsuarioCommand>
    {
        public Validator()
        {
            RuleFor(x => x.IdUsuario).NotEmpty().WithMessage("El IdDetalleUsuario es necesario");
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
