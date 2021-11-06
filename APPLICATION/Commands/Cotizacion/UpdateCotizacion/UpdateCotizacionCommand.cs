using FluentValidation;
using JKM.UTILITY.Utils;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;
using System;

namespace JKM.APPLICATION.Commands.Cotizacion.UpdateCotizacion
{
    public class UpdateCotizacionCommand : IRequest<ResponseModel>
    {
        [SwaggerSchema(ReadOnly = true)]
        public int IdCotizacion { get; set; }
        public string Solicitante { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public string Email { get; set; }
        public int IdCliente { get; set; }
        public double PrecioCotizacion { get; set; }
        public int IdTipoCotizacion { get; set; }
    }

    public class Validator : AbstractValidator<UpdateCotizacionCommand>
    {
        public Validator()
        {
            RuleFor(x => x.IdCotizacion)
                .GreaterThan(0).WithMessage("El IdCotizacion debe ser un entero positivo");
            RuleFor(x => x.Solicitante)
                .NotEmpty().WithMessage("El solicitante no puede ser vacío");
            RuleFor(x => x.Email)
               .NotEmpty().WithMessage("El email no puede ser vacío")
               .EmailAddress().WithMessage("Formato erroneo del correo");
            RuleFor(x => x.IdCliente)
                .NotEmpty().WithMessage("La empresa no puede estar vacía");
        }
    }
}
