using FluentValidation;
using FluentValidation.Results;
using JKM.PERSISTENCE.Repository.Notification;
using JKM.PERSISTENCE.Utils;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace JKM.APPLICATION.Commands.Notification.ContactUs
{
    public class ContactUsNotificationCommandHanlder : INotificationHandler<ContactUsNotificationCommand>
    {
        private readonly INotificationRepository _notificationRepository;

        public ContactUsNotificationCommandHanlder(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        public async Task Handle(ContactUsNotificationCommand request, CancellationToken cancellationToken)
        {
            ValidationResult validator = new Validator().Validate(request);
            Handlers.HandlerException(validator);

            NotificationModel model = new NotificationModel();
            model.ContactUs(request.EmailAddress, request.Empresa, request.Nombre, request.Apellido, request.Telefono, request.Mensaje, request.Path, request.Logo);
            await _notificationRepository.ContactUs(model);
        }

        private class Validator : AbstractValidator<ContactUsNotificationCommand>
        {
            public Validator()
            {
                RuleFor(x => x.EmailAddress)
                    .EmailAddress().WithMessage("El correo ingresado no es válido");
                RuleFor(x => x.Empresa)
                    .NotEmpty().WithMessage("Debe llenar la información de la empresa");
                RuleFor(x => x.Nombre)
                    .NotEmpty().WithMessage("El campo nombre no puede estar vacío");
                RuleFor(x => x.Apellido)
                    .NotEmpty().WithMessage("El campo apellidos no puede estar vacío");
                RuleFor(x => x.Mensaje)
                    .NotEmpty().WithMessage("Debe llenar la información del mensaje");
                RuleFor(x => x.Telefono)
                    .NotEmpty().WithMessage("El campo telefono no puede ser vacío")
                    .Must(x => x.Length == 9 && x.StartsWith("9")).WithMessage("Debe ingresar un telefono válido");
            }
        }
    }
}
