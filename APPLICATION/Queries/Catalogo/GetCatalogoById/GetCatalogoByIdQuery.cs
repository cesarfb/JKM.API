using FluentValidation;
using JKM.APPLICATION.Aggregates;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JKM.APPLICATION.Queries.Catalogo.GetCatalogoById
{
    public class GetCatalogoByIdQuery : IRequest<CatalogoModel>
    {
        public int IdCatalogo { get; set; }
    }

    public class Validator : AbstractValidator<GetCatalogoByIdQuery>
    {
        public Validator()
        {
            RuleFor(x => x.IdCatalogo).GreaterThan(0).WithMessage("El IdCatalogo debe ser un entero positivo");
        }
    }
}
