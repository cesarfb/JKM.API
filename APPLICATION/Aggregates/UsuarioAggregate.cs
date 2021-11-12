using System;
using System.Collections.Generic;
using System.Text;

namespace JKM.APPLICATION.Aggregates
{
    public class UsuarioModel
    {
        public int idUsuario { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public DateTime? fechaNacimiento { get; set; }
        public string fechaNacimientoString { get { return fechaNacimiento?.ToString("yyyy-MM-dd"); } }
        public string descripcion { get; set; }
    }

    public class DetalleUsuarioModel
    {
        public int idDetalleUsuario { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public DateTime? fechaNacimiento { get; set; }
        public string fechaNacimientoString { get { return fechaNacimiento?.ToString("yyyy-MM-dd"); } }

    }
}
