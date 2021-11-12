using FluentValidation;
using JKM.UTILITY.Utils;
using MediatR;
using System;

namespace JKM.APPLICATION.Commands.Venta.RegisterVenta
{
    public class RegisterVentaCommand : IRequest<ResponseModel>
    {

        public int IdVenta { get; set; }
        public int? NumeroCuota { get; set; }
        public double PagoParcial { get; set; }
        public DateTime FechaCuota { get; set; }

        //Tipo Proyecto
        public string NombreProyecto { get; set; }
        public string DescripcionProyecto { get; set; }

        //Tipo Pedido

        public int? IdProyecto { get; set; }
    }

    public class Validator : AbstractValidator<RegisterVentaCommand>
    {
        public Validator()
        {
            RuleFor(x => x.IdVenta).NotEmpty().WithMessage("El Id Venta no puede ser vacio");
            RuleFor(x => x.PagoParcial).NotEmpty().WithMessage("El Pago Parcial no puede ser vacio");
            RuleFor(x => x.FechaCuota).NotEmpty().WithMessage("La Fecha Cuota no puede ser vacio");
        }
    }
}
