using FluentValidation;
using JKM.APPLICATION.Aggregates;
using MediatR;

namespace JKM.APPLICATION.Queries.Trabajador.GetTrabajadorById
{
    public class GetTrabajadorByIdQuery : IRequest<TrabajadorModel>
    {
        public int IdTrabajador { get; set; }
    }

	public class Validator : AbstractValidator<GetTrabajadorByIdQuery>
	{
		public Validator()
		{
			RuleFor(x => x.IdTrabajador)
				.GreaterThan(0).WithMessage("El IdTrabajador debe ser un entero positivo");
		}
	}
}
