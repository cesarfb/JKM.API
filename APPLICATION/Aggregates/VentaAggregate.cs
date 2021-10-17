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
        [JsonProperty("fechaCuota")]
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
        public int idVenta { get; set; }
        public double precio { get; set; }

        private DateTime? FechaRegistro { get; set; }
        [JsonProperty("fechaRegistro")]
        public string FechaRegistroString
        {
            get
            {
                return FechaRegistro?.ToString("yyyy-MM-dd");
            }
        }

        private int idTipo { get; set; }
        private string tipoDescripcion { get; set; }

        private int idEstado { get; set; }
        private string estadoDescripcion { get; set; }


        public string razonSocial { get; set; }
        public string ruc { get; set; }

     
        public Identifier Estado
        {
            get
            {
                return new Identifier
                {
                    descripcion = estadoDescripcion,
                    id = idEstado
                };
            }
        }
        public Identifier Tipo
        {
            get
            {
                return new Identifier
                {
                    descripcion = tipoDescripcion,
                    id = idTipo
                };
            }
        }
    }
}
