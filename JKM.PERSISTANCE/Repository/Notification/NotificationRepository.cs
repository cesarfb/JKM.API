using JKM.PERSISTENCE.Utils;
using System.Data;
using System.Net.Mail;
using System.Threading.Tasks;

namespace JKM.PERSISTENCE.Repository.Notification
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly IDbConnection _conexion;
        private readonly SmtpClient _smtp;
        private readonly MailMessage _mail;

        public NotificationRepository(IDbConnection conexion, SmtpClient smtp, MailMessage mail)
        {
            _conexion = conexion;
            _smtp = smtp;
            _mail = mail;
        }

        public async Task ContactUs(NotificationModel notification)
        {
            using (MailMessage mail = _mail)
            {
                mail.To.Add(notification.EmailAddress);
                mail.Subject = notification.Subject;
                mail.Body = Templates.ContactUsHtml(notification);
                //byte[] pdf = Templates.ContactUsPdf(notification);
                //mail.Attachments.Add(new Attachment(pdf));

                using (SmtpClient smtp = _smtp)
                {
                    await smtp.SendMailAsync(mail);
                }
            }
        }
    }
}
