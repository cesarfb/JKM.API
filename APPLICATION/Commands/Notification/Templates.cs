using JKM.API.Reports;
using JKM.APPLICATION.Aggregates;
using JKM.APPLICATION.Commands.Notification.ContactUs;
using System;
using System.Collections.Generic;
using System.IO;
using IronPdf;
using System.Linq;
using IronPdf.Rendering;
using JKM.APPLICATION.Commands.Notification.RecuperarUsuario;
using JKM.APPLICATION.Commands.Notification.EnviarCotizacion;

namespace JKM.APPLICATION.Utils
{
    public static class Templates
    {
        public static string RecuperarUsuarioHtml(RecuperarUsuarioModel usuario)
        {
            return ReadPhysicalFile(Assets.RecuperarUsuarioHtml)
            .Replace("{LOGO}", Assets.Logo)
            .Replace("{NOMBRE}", usuario.Nombre)
            .Replace("{APELLIDO}", usuario.Apellido)
            .Replace("{USERNAME}", usuario.Username)
            .Replace("{PASSWORD}", usuario.Password);
        }

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

        public static string CotizacionHtml(EnviarCotizacionCommand cotizacion)
        {
            return ReadPhysicalFile(Assets.CotizacionHtml)
            .Replace("{LOGO}", Assets.Logo)
            .Replace("{EMPRESA}", cotizacion.Empresa)
            .Replace("{EMAIL}", cotizacion.EmailAddress)
            .Replace("{NOMBRE}", cotizacion.Nombre)
            .Replace("{TELEFONO}", cotizacion.Telefono)
            .Replace("{PRODUCTO_SERVICIO}", AddProductosServiciosCotizacion(cotizacion))
            .Replace("{MENSAJE}", cotizacion.Mensaje);
        }

        public static string AddProductosServiciosCotizacion(EnviarCotizacionCommand cotizacion)
        {
            string html = "";
            List<DetalleOrdenModel> products = new List<DetalleOrdenModel>();
            if (cotizacion.Productos.Count > 0)
            {
                cotizacion.Productos.ForEach(prod =>
                {
                    products.Add(new DetalleOrdenModel
                    {
                        precio = prod.Precio,
                        cantidad = prod.Cantidad,
                        codigoProducto = prod.Codigo,
                        idDetalleOrden = prod.IdCatalogo,
                        idProducto = prod.IdCatalogo,
                        imagen = prod.Imagen,
                        nombreProducto = prod.Nombre
                    });
                });
            }

            if (products.Count > 0)
            {
                html += $@"<tr>
                            <td>
                                <h3 style='padding: 20px 0;'> Productos </h3>
                                 <table class='center-table formulario'>
                                    <thead>
                                        <tr style = 'background-color: #7469e5;' >
                                            <th> Id </th>
                                            <th> Nombre </th>
                                            <th> Codigo </th>
                                            <th> Cantidad </th>
                                            <th> Precio U</th>
                                            <th> Precio T</th>
                                        </tr>
                                    </thead>";

                html += AddProduct(products);

                html += $@"</table>
                        </td>
                    </tr>";
            }

            if (cotizacion.Servicios.Count > 0)
            {
                html += $@"<tr>
                            <td>
                                <h3 style='padding: 20px 0;'> Servicios </h3>
                                 <table class='center-table formulario'>
                                    <thead>
                                        <tr style ='background-color: #7469e5;' >
                                            <th> Nombre </th>
                                            <th> Descripcion </th>
                                        </tr>
                                    </thead>";

                html += AddServicio(cotizacion.Servicios);
                html += $@" </table>
                        </td>
                    </tr>";
            }
            return html;
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

        private static string AddServicio(List<ServicioEmailModel> services)
        {
            if (services.Count == 0) return "";

            string serviceHtml = "";

            services.ForEach(serv =>
            {
                serviceHtml += $@" <tr>
                                        <td style='font-weight: bold;'> {serv.Nombre} </td>
                                        <td style='font-weight: bold;'> {serv.Descripcion} </td>
                                    </tr>";
            });
            return serviceHtml;
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
