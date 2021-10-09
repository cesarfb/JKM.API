using FluentValidation;
using JKM.PERSISTENCE.Utils;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace JKM.APPLICATION.Commands.Cotizacion.AceptarCotizacion
{
    public class AceptarCotizacionCommand : IRequest<ResponseModel>
    {
        public int IdCotizacion { get; set; }
    }
    public class Validator : AbstractValidator<AceptarCotizacionCommand>
    {
        public Validator()
        {
            RuleFor(x => x.IdCotizacion)
                .GreaterThan(0).WithMessage("El idCotizacion debe ser un entero positivo");
        }
    }
}
