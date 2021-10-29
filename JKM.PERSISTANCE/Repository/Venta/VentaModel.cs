using System;
using System.Collections.Generic;
using System.Text;

namespace JKM.PERSISTENCE.Repository.Venta
{
    public class VentaModel
    {
        public int IdVenta { get; set; }
        public int? NumeroCuota { get; set; }
        public double PagoParcial { get; set; }
        public DateTime? FechaCuota { get; set; }

        public void RegisterVenta(int idVenta = 0, double pagoParcial = 0.00, DateTime? fechaCuota = null)
        {
            PagoParcial = pagoParcial;
            FechaCuota = fechaCuota;
            IdVenta = idVenta;
        }

    }
}
