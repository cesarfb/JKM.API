using FluentValidation;
using JKM.UTILITY.Utils;
using MediatR;

namespace JKM.APPLICATION.Commands.Catalogo.UpdateEstadoCatalogo
{

    public class UpdateEstadoCatalogoCommand : IRequest<ResponseModel>
    {
        public int IdCatalogo { get; set; }
    }

    public class Validator : AbstractValidator<UpdateEstadoCatalogoCommand>
    {
        public Validator()
        {
            RuleFor(x => x.IdCatalogo).NotEmpty().WithMessage("El IdCatalogo es necesario");
        }

    }
}
