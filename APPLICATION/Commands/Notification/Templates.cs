using JKM.APPLICATION.Commands.Notification.ContactUs;
using System;
using System.IO;
//using TheArtOfDev.HtmlRenderer.PdfSharp;

namespace JKM.APPLICATION.Utils
{
    public static class Templates
    {
        public static string ContactUsHtml(ContactUsNotificationCommand model)
        {
            string emailMessage = ReadPhysicalFile(model.Path)
            .Replace("{LOGO}", model.Logo)
            .Replace("{EMPRESA}", model.Empresa)
            .Replace("{EMAIL}", model.EmailAddress)
            .Replace("{NOMBRE}", model.Nombre)
            .Replace("{TELEFONO}", model.Telefono.ToString())
            .Replace("{MENSAJE}", model.Mensaje);

            return emailMessage;
        }

        public static Byte[] ContactUsPdf(ContactUsNotificationCommand model)
        {
            Byte[] res;
            string html = ContactUsHtml(model);
            using (MemoryStream ms = new MemoryStream())
            {
                //var pdf = PdfGenerator.GeneratePdf(html, PdfSharp.PageSize.A4);
                //pdf.Save(ms);
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
