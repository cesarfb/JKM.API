using JKM.UTILITY.Utils;
using Newtonsoft.Json;
using System;

namespace JKM.APPLICATION.Aggregates
{
    public class VentaCuotasModel
    {
        public int IdVenta { get; set; }
        public int IdDetalleVenta { get; set; }
        public int NumeroCuota { get; set; }
        public double PagoParcial { get; set; }
        public DateTime? FechaCuota { get; set; }
        //[JsonProperty("fechaCuota")]
        public string FechaCuotaString
        {
            get
            {
                return FechaCuota?.ToString("yyyy-MM-dd");
            }
        }
    }
    public class VentaModel
    {
        public int IdVenta { get; set; }
        public double Precio { get; set; }

        private DateTime? FechaRegistro { get; set; }
        [JsonProperty("fechaRegistro")]
        public string FechaRegistroString
        {
            get
            {
                return FechaRegistro?.ToString("yyyy-MM-dd");
            }
        }

        private int IdTipo { get; set; }
        private string TipoDescripcion { get; set; }

        private int IdEstado { get; set; }
        private string EstadoDescripcion { get; set; }


        public string RazonSocial { get; set; }
        public string Ruc { get; set; }

     
        public Identifier Estado
        {
            get
            {
                return new Identifier
                {
                    descripcion = EstadoDescripcion,
                    id = IdEstado
                };
            }
        }
        public Identifier Tipo
        {
            get
            {
                return new Identifier
                {
                    descripcion = TipoDescripcion,
                    id = IdTipo
                };
            }
        }
    }
}
