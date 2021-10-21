using System;
using System.Collections.Generic;
using System.Text;

namespace JKM.APPLICATION.Aggregates
{
    public class WebClienteAggregate
    {

    }

    public class ServicioWebModel
    {
        public int IdServicio { get; set; }
        public string Nombre { get; set; }
        public string Imagen { get; set; }
        public string Descripcion { get; set; }
    }

    public class CatalogoWebModel
    {
        public int IdCatalogo { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public string Nombre { get; set; }
        public string Codigo { get; set; }
        public string Imagen { get; set; }
    }
}
