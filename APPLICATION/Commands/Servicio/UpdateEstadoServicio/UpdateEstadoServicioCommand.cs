using FluentValidation;
using JKM.UTILITY.Utils;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JKM.APPLICATION.Commands.Servicio.UpdateEstadoServicio
{
    public class UpdateEstadoServicioCommand : IRequest<ResponseModel>
    {
        public int IdServicio { get; set; }
    }

    public class Validator : AbstractValidator<UpdateEstadoServicioCommand>
    {
        public Validator()
        {
            RuleFor(x => x.IdServicio).NotEmpty().WithMessage("El IdServicio es necesario");
        }

    }
}
