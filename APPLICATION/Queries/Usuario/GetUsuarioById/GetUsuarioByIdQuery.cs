using FluentValidation;
using JKM.APPLICATION.Aggregates;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JKM.APPLICATION.Queries.Usuario.GetUsuarioById
{
    public class GetUsuarioByIdQuery : IRequest<UsuarioModel>
    {
        public int IdUsuario { get; set; }
    }

    public class Validator : AbstractValidator<GetUsuarioByIdQuery>
    {
        public Validator()
        {
            RuleFor(x => x.IdUsuario).GreaterThan(0).WithMessage("El idUsuario debe ser un entero positivo");
        }
    }
}
