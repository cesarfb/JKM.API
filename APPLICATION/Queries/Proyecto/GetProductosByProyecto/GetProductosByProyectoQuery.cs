using FluentValidation;
using JKM.APPLICATION.Aggregates;
using MediatR;
using System.Collections.Generic;

namespace JKM.APPLICATION.Queries.Proyecto.GetProductosByProyecto
{
    public class GetProductosByProyectoQuery : IRequest<IEnumerable<DetalleOrdenModel>>
	{
		public int IdProyecto { get; set; }
	}
	public class Validator : AbstractValidator<GetProductosByProyectoQuery>
	{
		public Validator()
		{
			RuleFor(x => x.IdProyecto)
				.GreaterThan(0).WithMessage("El IdProyecto debe ser un entero positivo");
		}
	}
}
