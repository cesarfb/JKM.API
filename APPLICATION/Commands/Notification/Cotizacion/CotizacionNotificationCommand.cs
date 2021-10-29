using FluentValidation;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;

namespace JKM.APPLICATION.Commands.Notification.Cotizacion
{
    public class CotizacionNotificationCommand : INotification
    {
        [SwaggerSchema(ReadOnly = true)]
        public string Path { get; set; }
        [SwaggerSchema(ReadOnly = true)]
        public string Logo { get; set; }
        public string EmailAddress { get; set; }
        public string Empresa { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public string Mensaje { get; set; }
        public List<ProductoCotizacionModel> Productos { get; set; }
        public List<ServicioCotizacionModel> Servicios { get; set; }
    }

    public class ProductoCotizacionModel
    {
        public int IdCatalogo { get; set; }
        public string Codigo { get; set; }
        public int Cantidad { get; set; }
        public string Imagen { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
    }

    public class ServicioCotizacionModel
    {
        public int IdServicio { get; set; }
        public string Imagen { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
    }

    public class CotizacionValidator : AbstractValidator<CotizacionNotificationCommand>
    {
        public CotizacionValidator()
        {
            RuleFor(x => x.EmailAddress)
                .EmailAddress().WithMessage("El correo ingresado no es válido");
            RuleFor(x => x.Empresa)
                .NotEmpty().WithMessage("Debe llenar la información de la empresa");
            RuleFor(x => x.Nombre)
                .NotEmpty().WithMessage("El campo nombre no puede estar vacío");
            RuleFor(x => x.Telefono)
                .NotEmpty().WithMessage("El campo telefono no puede ser vacío")
                .Must(x => x.Length == 9 && x.StartsWith("9")).WithMessage("Debe ingresar un telefono válido");
        }
    }
}
