using FluentValidation;
using JKM.APPLICATION.Aggregates;
using MediatR;

namespace JKM.APPLICATION.Queries.Auth.GetPerfilUsuario
{
    public class GetPerfilUsuarioQuery : IRequest<PerfilModel>
    {
        public int IdUsuario { get; set; }
    }
}