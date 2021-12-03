using FluentValidation;
using JKM.UTILITY.Utils;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JKM.APPLICATION.Commands.Catalogo.RegisterCatalogo
{
    public class RegisterCatalogoCommand : IRequest<ResponseModel>
    {
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public int IdProducto { get; set; }

    }

    public class Validator : AbstractValidator<RegisterCatalogoCommand>
    {
        public Validator()
        {
            RuleFor(x => x.Precio).NotEmpty().WithMessage("El Precio no puede ser vacio");
            RuleFor(x => x.Stock).NotEmpty().WithMessage("El Stock no puede ser vacio");
            RuleFor(x => x.IdProducto).NotEmpty().WithMessage("El IdProducto no puede ser vacio");
        }
    }
}
