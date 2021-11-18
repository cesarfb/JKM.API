using MediatR;
using System.Collections.Generic;

namespace JKM.APPLICATION.Commands.Notification.EnviarCotizacion
{
    public class EnviarCotizacionCommand : INotification
    {
        public string Nombre { get; set; }
        public string EmailAddress { get; set; }
        public string Telefono { get; set; }
        public string Empresa { get; set; }
        public string Mensaje { get; set; }
        public List<ProductoEmailModel> Productos { get; set; }
        public List<ServicioEmailModel> Servicios { get; set; }
    }

    public class ProductoEmailModel
    {
        public int IdCatalogo { get; set; }
        public string Codigo { get; set; }
        public string Imagen { get; set; }
        public string Nombre { get; set; }
        public decimal Precio { get; set; }
        public int Cantidad { get; set; }
    }

    public class ServicioEmailModel
    {
        public string Imagen { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
    }
}
