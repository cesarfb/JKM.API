using JKM.APPLICATION.Aggregates;
using JKM.APPLICATION.Utils;
using MediatR;
using System.Collections.Generic;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System;
using System.IO;

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

            var ran = new Random();

            string productPDf = "";
            string actividadesPdf = "";

            using (MailMessage mail = _mail)
            {
                var html = Templates.AceptarCotizacionHtml();
                    html = Templates.ProductosCotizacionPDF(html, productos.ToList());
                    html = Templates.ActividadesCotizacionPDF(html, actividades.ToList());

                mail.To.Add(cotizacion.email);
                mail.Subject = "Solicitud de Servicio";
                mail.Body = html;
                using (SmtpClient smtp = _smtp)
                {
                    await smtp.SendMailAsync(mail);
                }
            }

            if (String.IsNullOrEmpty(productPDf))
                File.Delete($@"Reports/Files/{productPDf}.pdf");

            if (String.IsNullOrEmpty(actividadesPdf))
                    File.Delete($@"Reports/Files/{actividadesPdf}.pdf");
        }
    }
}