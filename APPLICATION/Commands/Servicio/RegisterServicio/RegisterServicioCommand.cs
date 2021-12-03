using FluentValidation;
using JKM.UTILITY.Utils;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JKM.APPLICATION.Commands.Servicio.RegisterServicio
{
    public class RegisterServicioCommand : IRequest<ResponseModel>
    {
        public string Nombre { get; set; }
        public string Imagen { get; set; }
        public string Descripcion { get; set; }
        public int isActive { get; set; }
    }

    public class Validator : AbstractValidator<RegisterServicioCommand>
    {
        public Validator()
        {
            RuleFor(x => x.Nombre).NotEmpty().WithMessage("El Nombre no puede ser vacio");
            RuleFor(x => x.Descripcion).NotEmpty().WithMessage("La Descripcion no puede ser vacio");
        }
    }
}
