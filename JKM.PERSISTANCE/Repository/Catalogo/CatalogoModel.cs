using System;
using System.Collections.Generic;
using System.Text;

namespace JKM.PERSISTENCE.Repository.Catalogo
{
    public class CatalogoModel
    {
        public int IdCatalogo { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public int IdProducto { get; set; }

        public void RegisterCatalogo(decimal precio = 0, int stock = 0, int idProducto = 0)
        {
            Precio = precio;
            Stock = stock;
            IdProducto = idProducto;
        }

        public void UpdateCatalogo(int idCatalogo = 0, decimal precio = 0, int stock = 0, int idProducto = 0)
        {
            IdCatalogo = idCatalogo;
            Precio = precio;
            Stock = stock;
            IdProducto = idProducto;
        }
    }
}
