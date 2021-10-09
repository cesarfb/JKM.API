using FluentValidation;
using JKM.APPLICATION.Aggregates;
using MediatR;
using System.Collections.Generic;

namespace JKM.APPLICATION.Queries.Proyecto.GetTrabajadoresByProyecto
{
    public class GetTrabajadoresByProyectoQuery : IRequest<IEnumerable<TrabajadorProyectoModel>>
    {
        public int IdProyecto { get; set; }
    }
	public class Validator : AbstractValidator<GetTrabajadoresByProyectoQuery>
	{
		public Validator()
		{
			RuleFor(x => x.IdProyecto)
				.GreaterThan(0).WithMessage("El IdProyecto debe ser un entero positivo");
		}
	}
}
