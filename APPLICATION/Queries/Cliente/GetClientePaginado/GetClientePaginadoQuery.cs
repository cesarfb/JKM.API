using FluentValidation;
using JKM.APPLICATION.Aggregates;
using JKM.UTILITY.Utils;
using MediatR;

namespace JKM.APPLICATION.Queries.Cliente.GetClientePaginado
{
    public class GetClientePaginadoQuery : PaginadoModel, IRequest<PaginadoResponse<ClienteModel>>
    {
    }

    public class Validator : AbstractValidator<GetClientePaginadoQuery>
    {
        public Validator()
        {
            RuleFor(x => x.Pages)
                .GreaterThan(0).WithMessage("La cantidad de paginas debe ser un entero positivo");
            RuleFor(x => x.Rows)
                .GreaterThan(0).WithMessage("La cantidad de registros debe ser un entero positivo");
        }
    }
}
