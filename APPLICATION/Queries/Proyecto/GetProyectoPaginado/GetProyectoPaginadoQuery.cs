using FluentValidation;
using JKM.APPLICATION.Aggregates;
using JKM.UTILITY.Utils;
using MediatR;

namespace JKM.APPLICATION.Queries.Proyecto.GetProyectoPaginado
{
    public class GetProyectoPaginadoQuery : PaginadoModel, IRequest<PaginadoResponse<ProyectoModel>>
    {
    }

    public class Validator : AbstractValidator<GetProyectoPaginadoQuery>
    {
        public Validator()
        {
            RuleFor(x => x.Pages)
                .GreaterThan(0).WithMessage("La cantidad de paginas debe ser un entero positivo");
            RuleFor(x => x.Rows)
                .GreaterThan(0).WithMessage("La cantidad de paginas debe ser un entero positivo");
        }
    }
}
