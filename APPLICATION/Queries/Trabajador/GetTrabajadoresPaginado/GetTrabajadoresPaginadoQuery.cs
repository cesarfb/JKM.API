﻿using FluentValidation;
using JKM.APPLICATION.Aggregates;
using JKM.UTILITY.Utils;
using MediatR;

namespace JKM.APPLICATION.Queries.Trabajador.GetTrabajadoresPaginado
{
    public class GetTrabajadoresPaginadoQuery : PaginadoModel, IRequest<PaginadoResponse<TrabajadorModel>>
    {
        public int idEstado { get; set; }
        public int idTipo { get; set; }
    }

    public class Validator : AbstractValidator<GetTrabajadoresPaginadoQuery>
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
