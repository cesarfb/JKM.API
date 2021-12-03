using System;
using System.Collections.Generic;
using System.Text;

namespace JKM.PERSISTENCE.Repository.Servicio
{
    public class ServicioModel
    {
        public int IdServicio { get; set; }
        public string Nombre { get; set; }
        public string Imagen { get; set; }
        public string Descripcion { get; set; }
        public int IsActive { get; set; }

        public void RegisterServicio(string nombre = "", string imagen = "", string descripcion = "")
        {
            Nombre = nombre;
            Imagen = imagen;
            Descripcion = descripcion;
        }

        public void UpdateServicio(int idServicio = 0, string nombre = "", string imagen = "", string descripcion = "")
        {
            IdServicio = idServicio;
            Nombre = nombre;
            Imagen = imagen;
            Descripcion = descripcion;
        }
    }
}
