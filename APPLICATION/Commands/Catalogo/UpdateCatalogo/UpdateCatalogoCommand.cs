using FluentValidation;
using JKM.UTILITY.Utils;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JKM.APPLICATION.Commands.Catalogo.UpdateCatalogo
{
    public class UpdateCatalogoCommand : IRequest<ResponseModel>
    {
        public int IdCatalogo { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public int IdProducto { get; set; }
    }

    public class Validator : AbstractValidator<UpdateCatalogoCommand>
    {
        public Validator()
        {
            RuleFor(x => x.IdCatalogo).NotEmpty().WithMessage("El IdCatalogo no puede ser vacio");
            RuleFor(x => x.Precio).NotEmpty().WithMessage("El Precio no puede ser vacio");
            RuleFor(x => x.Stock).NotEmpty().WithMessage("El Stock no puede ser vacio");
            RuleFor(x => x.IdProducto).NotEmpty().WithMessage("El IdProducto no puede ser vacio");
        }
    }
}
