using JKM.PERSISTENCE.Repository.Notification;
using System;
using System.IO;

namespace JKM.PERSISTENCE.Utils
{
    public static class Templates
    {
        public static string ContactUsHtml(NotificationModel model)
        {
            string emailMessage = ReadPhysicalFile(model.Path)
            .Replace("{LOGO}", model.Logo)
            .Replace("{EMPRESA}", model.Empresa)
            .Replace("{EMAIL}", model.EmailAddress)
            .Replace("{NOMBRE}", model.Nombre)
            .Replace("{APELLIDO}", model.Apellido)
            .Replace("{TELEFONO}", model.Telefono.ToString())
            .Replace("{MENSAJE}", model.Mensaje);

            return emailMessage;
        }

        public static Byte[] ContactUsPdf(NotificationModel model)
        {
            Byte[] res = null;
            string html = ContactUsHtml(model);
            using (MemoryStream ms = new MemoryStream())
            {
                var pdf = TheArtOfDev.HtmlRenderer.PdfSharp.PdfGenerator.GeneratePdf(html, PdfSharp.PageSize.A4);
                pdf.Save(ms);
                res = ms.ToArray();
            }
            return res;
        }

        private static string ReadPhysicalFile(string path)
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException($"La plantilla del archivo localizado en \"{path}\" no fué encontrada.");
            }

            using (var fs = File.OpenRead(path))
            {
                using (var sr = new StreamReader(fs))
                {
                    return sr.ReadToEnd();
                }
            }
        }
    }
}
