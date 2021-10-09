using FluentValidation;
using JKM.APPLICATION.Aggregates;
using MediatR;
using System.Collections.Generic;

namespace JKM.APPLICATION.Queries.Trabajador.GetTrabajadorByEstado
{
    public class GetTrabajadorByEstadoQuery : IRequest<IEnumerable<TrabajadorModel>>
    {
        public int IdEstado { get; set; }
    }
    public class Validator : AbstractValidator<GetTrabajadorByEstadoQuery>
    {
        public Validator()
        {
            RuleFor(x => x.IdEstado)
                .GreaterThan(0).WithMessage("El IdEstado debe ser un entero positivo");
        }
    }
}
