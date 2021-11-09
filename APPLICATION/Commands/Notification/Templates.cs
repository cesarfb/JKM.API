using JKM.API.Reports;
using JKM.APPLICATION.Aggregates;
using JKM.APPLICATION.Commands.Notification.ContactUs;
using System;
using System.Collections.Generic;
using System.IO;
using IronPdf;
using System.Linq;
using IronPdf.Rendering;

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

        public static void ProductosCotizacionPDF(CotizacionModel empresa, List<DetalleOrdenModel> productos, string nameFile)
        {
            string html = ReadPhysicalFile(Assets.TemplateProductosCotizacionPDF)
            .Replace("{LOGO}", Assets.Logo)
            .Replace("{ORDER}", empresa.idCotizacion.ToString())
            .Replace("{DATE}", DateTime.Now.ToString("dd/MM/yyyy"))
            .Replace("{EMPRESA}", empresa.razonSocial)
            .Replace("{SOLICITANTE}", empresa.solicitante)
            .Replace("{PRODUCTOS}", AddProduct(productos));

            HtmlToPdf(html, nameFile);
        }

        private static string AddProduct(List<DetalleOrdenModel> products)
        {
            decimal precioTotal = 0;
            if (products.Count == 0) return "";

            string productHtml = "";

            products.ForEach(prod =>
            {
                precioTotal += prod.precio * prod.cantidad;
                productHtml += $@" <tr>
                                        <td style='font-weight: bold;'> {prod.idProducto} </td>
                                        <td style='font-weight: bold;'> {prod.nombreProducto} </td>
                                        <td> {prod.codigoProducto} </td>
                                        <td> {prod.cantidad} u</td>
                                        <td> S/ {prod.precio} </td>
                                        <td> S/ {prod.precio * prod.cantidad} </td>
                                    </tr>";
            });
            productHtml += $@" <tr style='border-top: 1px solid #7469e5'>
                                   <td colspan='4'></td >
                                   <td style='font-weight: bold;'> Total </td >
                                   <td style='font-weight: bold;'> S/ {precioTotal} </td>
                               </tr>";
            return productHtml;
        }

        public static void ActividadesCotizacionPDF(CotizacionModel empresa, List<ActividadCotizancionTreeNode> actividades, string nameFile)
        {
            string html = ReadPhysicalFile(Assets.TemplateActividadesCotizacionPDF)
            .Replace("{LOGO}", Assets.Logo)
            .Replace("{ORDER}", empresa.idCotizacion.ToString())
            .Replace("{DATE}", DateTime.Now.ToString("dd/MM/yyyy"))
            .Replace("{EMPRESA}", empresa.razonSocial)
            .Replace("{SOLICITANTE}", empresa.solicitante)
            .Replace("{ACTIVIDADES}", AddActividades(actividades));

            HtmlToPdf(html, nameFile);
        }

        private static string AddActividades(List<ActividadCotizancionTreeNode> actividades)
        {
            if (actividades.Count == 0) return "";

            string actividadesHtml = "";

            int index = 0;
            actividades.ForEach(activ =>
            {
                index++;

                var data = activ.data;
                var children = activ.children.ToList();
                actividadesHtml += $@"<h4 style='margin: 10px 0;'> 
                                         Actividad {index}: {data.Descripcion} <span style='font-weight: bold;'>({data.Prioridad})</span> 
                                      </h4>";

                if (children.Count > 0)
                {
                    actividadesHtml += $@"<table class='border-row'>
                                            <thead>
                                                <tr>
                                                    <th> Nombre de la Tarea </th>
                                                    <th> Prioridad</th>
                                                    <th> Estado </th>
                                                    <th> Subtareas </th>
                                                </tr>
                                            </thead>
                                            <tbody>";
                    children.ForEach(child =>
                    {
                        actividadesHtml += $@"<tr>
                                            <td style='font-weight: bold;'> {child.data.Descripcion} </td>
                                            <td> {child.data.Prioridad} </td>
                                            <td> {child.data.DescripcionEstado}</td>
                                            <td>";
                        var subActiv = child.children.ToList();
                        if (subActiv.Count > 0)
                        {
                            actividadesHtml += $@"<ul>";
                            subActiv.ForEach(sub =>
                            {
                                actividadesHtml += $@"<li>{sub.data.Descripcion} ({sub.data.DescripcionEstado})</li>";
                            });
                            actividadesHtml += "</ul>";
                        }
                        actividadesHtml += $@"</td>
                                         </tr>
                                    </tbody>
                                </table>";
                    });
                }
            });
            return actividadesHtml;
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
            var Renderer = new ChromePdfRenderer();
            Renderer.RenderingOptions.PaperSize = PdfPaperSize.A3;
            Renderer.RenderHtmlAsPdf(html).SaveAs($@"Reports/Files/{nameFile}.pdf");
        }
    }
}
