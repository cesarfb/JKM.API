using System;

namespace JKM.PERSISTENCE.Repository.Trabajador
{
    public class TrabajadorModel
    {
        public int IdTrabajador { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public int IdTipo { get; set; }
        public int IdEstado { get; set; }
        public void Register(string nombre, string apellidoPaterno, string apellidoMaterno, string fechaNacimiento, int idTipo)
        {
            Nombre = nombre;
            ApellidoPaterno = apellidoPaterno;
            ApellidoMaterno = apellidoMaterno;
            FechaNacimiento = DateTime.Parse(fechaNacimiento);
            IdTipo = idTipo;
            IdEstado = 2;
        }
        public void Update(int idTrabajador, string nombre, string apellidoPaterno, string apellidoMaterno, string fechaNacimiento, int idTipo, int idEstado)
        {
            IdTrabajador = idTrabajador;
            Nombre = nombre;
            ApellidoPaterno = apellidoPaterno;
            ApellidoMaterno = apellidoMaterno;
            FechaNacimiento = DateTime.Parse(fechaNacimiento);
            IdTipo = idTipo;
            IdEstado = idEstado;
        }
    }

    public class TipoTrabajadorProyectoModel
    {
        public int IdTipoTrabajador { get; set; }
        public string Nombre { get; set; }
        public string Descripcion{ get; set; }
        public decimal PrecioReferencial{ get; set; }
        public void Register(string nombre, string descripcion, decimal precioReferencial)
        {
            Nombre = nombre;
            Descripcion = descripcion;
            PrecioReferencial = precioReferencial;
        }
        public void Update(int idTipoTrabajador, string nombre, string descripcion, decimal precioReferencial)
        {
            IdTipoTrabajador = idTipoTrabajador;
            Nombre = nombre;
            Descripcion = descripcion;
            PrecioReferencial = precioReferencial;
        }
    }
}
