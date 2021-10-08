using System;
using System.Collections.Generic;
using System.Text;

namespace JKM.PERSISTENCE.Repository.Notification
{
    public class NotificationModel
    {
        public string EmailAddress { get; set; }
        public string To { get; set; }
        public string Empresa { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int Telefono { get; set; }
        public string Subject { get; set; }
        public string Mensaje { get; set; }
        public string Path { get; set; }
        public string Logo { get; set; }
        public string FechaDeSolicitud
        {
            get
            {
                return DateTime.Now.ToString("dd-MM-yyyy t");
            }
        }
        public void ContactUs(string emailAddress, string empresa, string nombre, string apellido, string telefono, string mensaje, string path, string logo)
        {
            EmailAddress = emailAddress;
            Empresa = empresa;
            Nombre = nombre;
            Apellido =  apellido;
            Telefono = int.Parse(telefono);
            Subject = "Solicitud de Servicio";
            Mensaje = mensaje;
            Path = path;
            Logo = logo;
        }

    }
}
