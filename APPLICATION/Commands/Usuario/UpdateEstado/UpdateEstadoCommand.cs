using FluentValidation;
using JKM.UTILITY.Utils;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JKM.APPLICATION.Commands.Usuario.UpdateEstado
{
    public class UpdateEstadoCommand : IRequest<ResponseModel>
    {
        public int IdUsuario { get; set; }
    }

    public class Validator : AbstractValidator<UpdateEstadoCommand>
    {
        public Validator()
        {
            RuleFor(x => x.IdUsuario).NotEmpty().WithMessage("El IdUsuario es necesario");
        }

    }

}
