using FluentValidation;
using JKM.APPLICATION.Aggregates;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JKM.APPLICATION.Queries.Servicio.GetServicioById
{
    public class GetServicioByIdQuery : IRequest<ServicioModel>
    {
        public int IdServicio { get; set; }
    }

    public class Validator : AbstractValidator<GetServicioByIdQuery>
    {
        public Validator()
        {
            RuleFor(x => x.IdServicio).GreaterThan(0).WithMessage("El IdServicio debe ser un entero positivo");
        }
    }
}
