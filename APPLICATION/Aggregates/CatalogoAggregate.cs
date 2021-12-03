using System;
using System.Collections.Generic;
using System.Text;

namespace JKM.APPLICATION.Aggregates
{
    public class CatalogoModel : ProductoModel
    {
        public int idCatalogo { get; set; }
        public decimal precio { get; set; }
        public int stock { get; set; }
        public int isActive { get; set; }

    }
}
