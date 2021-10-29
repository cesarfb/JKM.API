using FluentValidation;
using JKM.APPLICATION.Aggregates;
using JKM.UTILITY.Utils;
using MediatR;

namespace JKM.APPLICATION.Queries.Cliente.GetClientePaginado
{
    public class GetClientePaginadoQuery : IRequest<PaginadoResponse<ClienteModel>>
    {
    }
}
