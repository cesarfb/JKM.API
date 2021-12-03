using FluentValidation;
using JKM.UTILITY.Utils;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JKM.APPLICATION.Commands.Servicio.UpdateServicio
{
    public class UpdateServicioCommand : IRequest<ResponseModel>
    {
        public int IdServicio { get; set; }
        public string Nombre { get; set; }
        public string Imagen { get; set; }
        public string Descripcion { get; set; }
    }

    public class Validator : AbstractValidator<UpdateServicioCommand>
    {
        public Validator()
        {
            RuleFor(x => x.IdServicio).NotEmpty().WithMessage("El IdServicio no puede ser vacio");
            RuleFor(x => x.Nombre).NotEmpty().WithMessage("El Nombre no puede ser vacio");
            RuleFor(x => x.Descripcion).NotEmpty().WithMessage("El Descripcion no puede ser vacio");
        }
    }
}
