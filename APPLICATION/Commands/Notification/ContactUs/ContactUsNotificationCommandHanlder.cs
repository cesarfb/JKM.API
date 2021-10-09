using JKM.PERSISTENCE.Repository.Notification;
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
            NotificationModel model = new NotificationModel();
            model.ContactUs(request.EmailAddress, request.Empresa, request.Nombre, request.Apellido, request.Telefono, request.Mensaje, request.Path, request.Logo);
            await _notificationRepository.ContactUs(model);
        }
    }
}
