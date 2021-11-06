using FluentValidation;
using MediatR;

namespace JKM.APPLICATION.Commands.Notification.ContactUs
{
    public class ContactUsNotificationCommand : INotification
    {
        public string EmailAddress { get; set; }
        public string Empresa { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public string Mensaje { get; set; }
    }

    public class ContactUsValidator : AbstractValidator<ContactUsNotificationCommand>
    {
        public ContactUsValidator()
        {
            RuleFor(x => x.EmailAddress)
                .EmailAddress().WithMessage("El correo ingresado no es válido");
            RuleFor(x => x.Empresa)
                .NotEmpty().WithMessage("Debe llenar la información de la empresa");
            RuleFor(x => x.Nombre)
                .NotEmpty().WithMessage("El campo nombre no puede estar vacío");
            RuleFor(x => x.Mensaje)
                .NotEmpty().WithMessage("Debe llenar la información del mensaje");
            RuleFor(x => x.Telefono)
                .NotEmpty().WithMessage("El campo telefono no puede ser vacío")
                .Must(x => x.Length == 9 && x.StartsWith("9")).WithMessage("Debe ingresar un telefono válido");
        }
    }
}
