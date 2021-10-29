using FluentValidation;
using JKM.UTILITY.Utils;
using MediatR;
using System;

namespace JKM.APPLICATION.Commands.Cotizacion.RegisterCotizacion
{
    public class RegisterCotizacionCommand : IRequest<ResponseModel>
    {
        public string Solicitante { get; set; }
        public string Descripcion { get; set; }
        public DateTime? FechaSolicitud { get; set; }
        public string Email { get; set; }
        public int IdCliente { get; set; }
        public double PrecioCotizacion { get; set; }
    }

    public class Validator : AbstractValidator<RegisterCotizacionCommand>
    {
        public Validator()
        {
            RuleFor(x => x.Solicitante)
                .NotEmpty().WithMessage("El solicitante no puede ser vacío");
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("El Email no puede ser vacío")
                .EmailAddress().WithMessage("Formato incorrecto del correo");
            RuleFor(x => x.IdCliente)
                .NotEmpty().WithMessage("El cliente no puede ser vacío");
        }
    }
}
