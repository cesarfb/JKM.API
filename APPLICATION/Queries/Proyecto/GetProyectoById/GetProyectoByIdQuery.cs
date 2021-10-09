using FluentValidation;
using JKM.APPLICATION.Aggregates;
using MediatR;

namespace JKM.APPLICATION.Queries.Proyecto.GetProyectoById
{
    public class GetProyectoByIdQuery : IRequest<ProyectoModel>
    {
        public int IdProyecto { get; set; }
    }
    public class Validator : AbstractValidator<GetProyectoByIdQuery>
    {
        public Validator()
        {
            RuleFor(x => x.IdProyecto)
                .GreaterThan(0).WithMessage("El IdProyecto debe ser un entero positivo");
        }
    }
}
