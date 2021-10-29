using JKM.APPLICATION.Utils;
using MediatR;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;

namespace JKM.APPLICATION.Commands.Notification.Cotizacion
{
    public class CotizacionNotificationCommandHandler : INotificationHandler<CotizacionNotificationCommand>
    {
        private readonly SmtpClient _smtp;
        private readonly MailMessage _mail;
        public CotizacionNotificationCommandHandler(SmtpClient smtp, MailMessage mail)
        {
            _smtp = smtp;
            _mail = mail;
        }

        public async Task Handle(CotizacionNotificationCommand request, CancellationToken cancellationToken)
        {
            using (MailMessage mail = _mail)
            {
                mail.To.Add(request.EmailAddress);
                mail.Subject = "Solicitud de Cotizacion";
                mail.AlternateViews.Add(Templates.CotizaciontUsHtml(request));
                using (SmtpClient smtp = _smtp)
                {
                    await smtp.SendMailAsync(mail);
                }
            }
        }
    }
}
