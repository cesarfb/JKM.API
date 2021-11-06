using FluentValidation;
using JKM.APPLICATION.Aggregates;
using JKM.UTILITY.Utils;
using MediatR;
using System.Collections.Generic;

namespace JKM.APPLICATION.Queries.Cliente.GetClientePaginado
{
    public class GetClienteQuery : IRequest<IEnumerable<ClienteModel>>
    {
    }
}
