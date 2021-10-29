using JKM.APPLICATION.Commands.Notification.ContactUs;
using JKM.APPLICATION.Commands.Notification.Cotizacion;
using System;
using System.Collections.Generic;
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

            AlternateView alternateView = AlternateView.CreateAlternateViewFromString(html, null, MediaTypeNames.Text.Html);

            alternateView.LinkedResources.Add(resource);

            return alternateView;
        }

        public static AlternateView CotizaciontUsHtml(CotizacionNotificationCommand model)
        {
            LinkedResource resource = CreateResource(model.Logo);
           
            string html = ReadPhysicalFile(model.Path)
            .Replace("{LOGO}", resource.ContentId)
            .Replace("{EMPRESA}", model.Empresa)
            .Replace("{EMAIL}", model.EmailAddress)
            .Replace("{NOMBRE}", model.Nombre)
            .Replace("{TELEFONO}", model.Telefono.ToString())
            .Replace("{MENSAJE}", model.Mensaje)
            .Replace("{PRODUCTOS}", AddProduct(model.Productos))
            .Replace("{SERVICIOS}", AddService(model.Servicios));

            AlternateView alternateView = AlternateView.CreateAlternateViewFromString(html, null, MediaTypeNames.Text.Html);

            alternateView.LinkedResources.Add(resource);

            return alternateView;
        }

        private static string AddProduct(List<ProductoCotizacionModel> products)
        {
            if (products.Count == 0) return "";
            string productHtml = $@"<tr>
                                        <td>
                                            <h2> Productos </h2>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class='center-text tittle'>
                                            <table class='center-table formulario'>
                                                <thead>
                                                    <td>SKU</td>
                                                    <td>NOMBRE</td>
                                                    <td>CANTIDAD</td>
                                                    <td>PRECIO U</td>
                                                    <td>PRECIO T</td>
                                                </thead>
                                                <tbody>";
                                             
            products.ForEach(prod =>
            {
                productHtml += $@" <tr>
                                       <td>{prod.Codigo}</td>
                                       <td>{prod.Nombre}</td>
                                       <td>{prod.Cantidad}</td>
                                       <td>S/ {prod.Precio}</td>
                                       <td>S/ {prod.Precio * prod.Cantidad}</td>
                                   </tr>";
            });
            productHtml += @"</tbody>
                             </table>
                             </td>
                             </tr>";
            return productHtml;
        }

        private static string AddService(List<ServicioCotizacionModel> services)
        {
            if (services.Count == 0) return "";
            string serviceHtml = $@"<tr>
                                        <td>
                                            <h2> Servicios </h2>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class='center-text tittle'>
                                            <table class='center-table formulario'>
                                                <thead>
                                                    <td>NOMBRE</td>
                                                    <td>DESCRIPCION</td>
                                                </thead>
                                                <tbody>";

            services.ForEach(serv =>
            {
                serviceHtml += $@" <tr>
                                       <td>{serv.Nombre}</td>
                                       <td>{serv.Descripcion}</td>
                                   </tr>";
            });
            serviceHtml += @"</tbody>
                             </table>
                             </td>
                             </tr>";
            return serviceHtml;
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

        private static LinkedResource CreateResource(string path)
        {
            LinkedResource res = new LinkedResource(path);
            res.ContentId = Guid.NewGuid().ToString();
            return res;
        }
    }
}
