using FluentValidation;
using JKM.UTILITY.Utils;
using MediatR;
using System;

namespace JKM.APPLICATION.Commands.Trabajador.RegisterTipoTrabajador
{
    public class RegisterTipoTrabajadorCommand : IRequest<ResponseModel>
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal PrecioReferencial { get; set; }
    }

    public class Validator : AbstractValidator<RegisterTipoTrabajadorCommand>
    {
        public Validator()
        {
            RuleFor(x => x.Nombre)
                .NotEmpty()
                .WithMessage("El nombre no puede ser vacio");
            RuleFor(x => x.Descripcion)
                .NotEmpty()
                .WithMessage("La descripcion paterno no puede ser vacio");
            RuleFor(x => x.PrecioReferencial)
                .NotEmpty()
                .GreaterThan(0)
                .WithMessage("El Precio debe ser un numero mayor a 0");
        }
    }
}