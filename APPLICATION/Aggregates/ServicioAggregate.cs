using System;
using System.Collections.Generic;
using System.Text;

namespace JKM.APPLICATION.Aggregates
{
    public class ServicioModel
    {
        public int idServicio { get; set; }
        public string nombre { get; set; }
        public string imagen { get; set; }
        public string descripcion { get; set; }
        public int isActive { get; set; }

    }
}
