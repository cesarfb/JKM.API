using System;
using System.Collections.Generic;
using System.Text;

namespace JKM.APPLICATION.Aggregates
{
    public class UsuarioModel : DetalleUsuarioModel
    {
        public int idUsuario { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public int idRol { get; set; }
        public string descripcionRol { get; set; }
        public string descripcionEstado { get; set; }
    }

    public class DetalleUsuarioModel
    {
        public int idDetalleUsuario { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string email { get; set; }
        public DateTime? fechaNacimiento { get; set; }
        public string fechaNacimientoString { get { return fechaNacimiento?.ToString("dd/MM/yyyy"); } }

    }
}
