using JKM.API.Reports;
using JKM.APPLICATION.Aggregates;
using JKM.APPLICATION.Commands.Notification.ContactUs;
using PdfSharp;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.IO;
using TheArtOfDev.HtmlRenderer.PdfSharp;

namespace JKM.APPLICATION.Utils
{
    public static class Templates
    {
        public static string ContactUsHtml(ContactUsNotificationCommand model)
        {
            return ReadPhysicalFile(Assets.ContactUsHtml)
            .Replace("{LOGO}", Assets.Logo)
            .Replace("{EMPRESA}", model.Empresa)
            .Replace("{EMAIL}", model.EmailAddress)
            .Replace("{NOMBRE}", model.Nombre)
            .Replace("{TELEFONO}", model.Telefono.ToString())
            .Replace("{MENSAJE}", model.Mensaje);
        }

        public static string CotizacionHtml(CotizacionModel cotizacion, IEnumerable<TipoTrabajadorModel> trabajadores)
        {
            return ReadPhysicalFile(Assets.CotizacionHtml)
            .Replace("{LOGO}", Assets.Logo);
            //.Replace("{PRODUCTOS}, )
        }

        public static void ProductosCotizacionPDF(CotizacionModel empresa,List<DetalleOrdenModel> productos, string nameFile)
        {
            string html = ReadPhysicalFile(Assets.TemplateProductosCotizacionPDF)
            .Replace("{LOGO}", Assets.Logo)
            .Replace("{ORDER}", empresa.idCotizacion.ToString())
            .Replace("{DATE}", DateTime.Now.ToString("dd/MM/yyyy"))
            .Replace("{EMPRESA}", empresa.razonSocial)
            .Replace("{SOLICITANTE}", empresa.solicitante)
            .Replace("{PRECIOTOTAL}", empresa.precioCotizacion.ToString())
            .Replace("{PRODUCTOS}", AddProduct(productos));

            HtmlToPdf(html, nameFile);
        }

        private static string AddProduct(List<DetalleOrdenModel> products)
        {
            if (products.Count == 0) return "";

            string productHtml = "";

            products.ForEach(prod =>
            {
                productHtml += $@" <tr>
                                        <td> {prod.idProducto} </td>
                                        <td> {prod.nombreProducto} </td>
                                        <td> {prod.codigoProducto} </td>
                                        <td> {prod.cantidad} u</td>
                                        <td> S/ {prod.precio} </td>
                                        <td> S/ {prod.precio * prod.cantidad} </td>
                                    </tr>";
            });
            return productHtml;
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

        private static void HtmlToPdf(string html, string nameFile)
        {
            PdfDocument pdfDocument = PdfGenerator.GeneratePdf(html, PageSize.A3, margin: 40);
            pdfDocument.Save($"Reports/Files/{nameFile}");
        }

    }
}
