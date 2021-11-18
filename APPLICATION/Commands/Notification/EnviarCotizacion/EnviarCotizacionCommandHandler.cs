using JKM.APPLICATION.Utils;
using MediatR;
using System;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;

namespace JKM.APPLICATION.Commands.Notification.EnviarCotizacion
{
    public class EnviarCotizacionCommandHandler : INotificationHandler<EnviarCotizacionCommand>
    {
        private readonly SmtpClient _smtp;
        private readonly MailMessage _mail;

        public EnviarCotizacionCommandHandler(SmtpClient smtp, MailMessage mail)
        {
            _smtp = smtp;
            _mail = mail;
        }
        public async Task Handle(EnviarCotizacionCommand request, CancellationToken cancellationToken)
        {
            using (MailMessage mail = _mail)
            {
                mail.To.Add(request.EmailAddress);
                mail.Subject = "Solicitud de Servicio";
                mail.Body = Templates.CotizacionHtml(request);
                using (SmtpClient smtp = _smtp)
                {
                    await smtp.SendMailAsync(mail);
                }
            }
        }
    }
}
