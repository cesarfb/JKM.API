using JKM.APPLICATION.Commands.Notification.ContactUs;
using System;
using System.IO;
using System.Net.Mail;
using System.Net.Mime;

namespace JKM.APPLICATION.Utils
{
    public static class Templates
    {
        public static AlternateView ContactUsHtml(ContactUsNotificationCommand model)
        {
            LinkedResource resource = CreateResource(model.Logo);
            string html = ReadPhysicalFile(model.Path)
            .Replace("{LOGO}", resource.ContentId)
            .Replace("{EMPRESA}", model.Empresa)
            .Replace("{EMAIL}", model.EmailAddress)
            .Replace("{NOMBRE}", model.Nombre)
            .Replace("{TELEFONO}", model.Telefono.ToString())
            .Replace("{MENSAJE}", model.Mensaje);

            //  string html = ReadPhysicalFile(model.Path)
            //.Replace("{LOGO}", ImageToBase64(model.Logo, "png"))
            //.Replace("{EMPRESA}", model.Empresa)
            //.Replace("{EMAIL}", model.EmailAddress)
            //.Replace("{NOMBRE}", model.Nombre)
            //.Replace("{TELEFONO}", model.Telefono.ToString())
            //.Replace("{MENSAJE}", model.Mensaje);


            AlternateView alternateView = AlternateView.CreateAlternateViewFromString(html, null, MediaTypeNames.Text.Html);

            alternateView.LinkedResources.Add(resource);

            return alternateView;
            //return html;
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

        public static LinkedResource CreateResource(string path)
        {
            LinkedResource res = new LinkedResource(path);
            res.ContentId = Guid.NewGuid().ToString();
            return res;
        }

        public static string ImageToBase64(string imgPath, string extension)
        {
            byte[] imageBytes = File.ReadAllBytes(imgPath);
            return $"data:image/{extension};base64," + Convert.ToBase64String(imageBytes);
        }


        //public static Byte[] HtmlToPdf(string html)
        //{
        //    Byte[] res = null;
        //    using (MemoryStream ms = new MemoryStream())
        //    {
        //        var pdf = TheArtOfDev.HtmlRenderer.PdfSharp.PdfGenerator.GeneratePdf(html, PdfSharp.PageSize.A4);
        //        pdf.Save(ms);
        //        res = ms.ToArray();
        //    }
        //    return res;
        //}
    }
}
