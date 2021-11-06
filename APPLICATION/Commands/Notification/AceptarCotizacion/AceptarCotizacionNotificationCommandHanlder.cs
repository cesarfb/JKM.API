using JKM.APPLICATION.Aggregates;
using JKM.APPLICATION.Utils;
using MediatR;
using System.Collections.Generic;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace JKM.APPLICATION.Commands.Notification.AceptarCotizacion
{
    public class AceptarCotizacionNotificationCommandHanlder : INotificationHandler<AceptarCotizacionNotificationCommand>
    {
        private readonly SmtpClient _smtp;
        private readonly MailMessage _mail;

        public AceptarCotizacionNotificationCommandHanlder(SmtpClient smtp, MailMessage mail)
        {
            _smtp = smtp;
            _mail = mail;
        }

        public async Task Handle(AceptarCotizacionNotificationCommand request, CancellationToken cancellationToken)
        {
            CotizacionModel cotizacion = request.Cotizacion;
            IEnumerable<TipoTrabajadorModel> trabajadores = request.Trabajadores;
            IEnumerable<ActividadCotizancionTreeNode> actividades = request.Actividades;
            IEnumerable<DetalleOrdenModel> productos = request.Productos;

            if (productos.ToList().Count > 0)
            {
                Templates.ProductosCotizacionPDF(cotizacion, productos.ToList(), "productos.pdf");
            }
            if (actividades.ToList().Count > 0)
            {

            }

            using (MailMessage mail = _mail)
            {
                mail.To.Add(cotizacion.email);
                mail.Subject = "Solicitud de Servicio";
                mail.Body = Templates.CotizacionHtml(cotizacion, trabajadores);


                using (SmtpClient smtp = _smtp)
                {
                    await smtp.SendMailAsync(mail);
                }
            }
        }
    }
}