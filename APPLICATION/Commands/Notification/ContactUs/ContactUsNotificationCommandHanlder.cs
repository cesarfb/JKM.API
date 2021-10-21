using JKM.APPLICATION.Utils;
using MediatR;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;

namespace JKM.APPLICATION.Commands.Notification.ContactUs
{
    public class ContactUsNotificationCommandHanlder : INotificationHandler<ContactUsNotificationCommand>
    {
        private readonly SmtpClient _smtp;
        private readonly MailMessage _mail;

        public ContactUsNotificationCommandHanlder(SmtpClient smtp, MailMessage mail)
        {
            _smtp = smtp;
            _mail = mail;
        }

        public async Task Handle(ContactUsNotificationCommand request, CancellationToken cancellationToken)
        {
            using (MailMessage mail = _mail)
            {
                mail.To.Add(request.EmailAddress);
                mail.Subject = "Solicitud de Servicio";
                mail.AlternateViews.Add(Templates.ContactUsHtml(request));
                //mail.Body = Templates.ContactUsHtml(request);
                using (SmtpClient smtp = _smtp)
                {
                    await smtp.SendMailAsync(mail);
                }
            }
        }
    }
}
