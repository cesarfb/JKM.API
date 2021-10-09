using FluentValidation;
using JKM.APPLICATION.Aggregates;
using MediatR;
using System.Collections.Generic;

namespace JKM.APPLICATION.Queries.Proyecto.GetActividadesByProyecto
{
    public class GetActividadesByProyectoQuery : IRequest<IEnumerable<ActividadProyectoModel>>
    {
        public int IdProyecto { get; set; }
    }

	public class Validator : AbstractValidator<GetActividadesByProyectoQuery>
	{
		public Validator()
		{
			RuleFor(x => x.IdProyecto)
				.GreaterThan(0).WithMessage("El IdProyecto debe ser un entero positivo");
		}
	}
}
