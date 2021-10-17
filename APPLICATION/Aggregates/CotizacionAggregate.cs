using JKM.UTILITY.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace JKM.APPLICATION.Aggregates
{
    public class CotizacionModel
    {
        public int idCotizacion { get; set; }
        public string solicitante { get; set; }
        public string descripcion { get; set; }
        private DateTime fechaSolicitud { get; set; }
        [JsonProperty("fechaNacimiento")]
        public string fechaSolicitudString
        {
            get
            {
                return fechaSolicitud.ToString("yyyy-MM-dd");
            }
        }
        public string email { get; set; }
        public string empresa { get; set; }
        public Identifier Estado
        {
            get
            {
                return new Identifier
                {
                    id = idEstado,
                    descripcion = descripcionEstado
                };
            }
        }

        public IdentifierPrecio Precio
        {
            get
            {
                return new IdentifierPrecio
                {
                    id = idPrecioCotizacion,
                    precio = precioCotizacion
                };
            }
        }

        private int idEstado { get; set; }
        private string descripcionEstado { get; set; }
        private int idPrecioCotizacion { get; set; }
        private double precioCotizacion { get; set; }
        public bool canCotizar { get; set; }
        public bool canDelete { get; set; }
        public bool canEdit { get; set; }
    }
    public class ActividadCotizacionModel
    {
        public int IdCotizacion { get; set; }
        public int IdActividad { get; set; }
        public string Descripcion { get; set; }
        public int Peso { get; set; }
        public string Prioridad
        {
            get
            {
                switch (Peso)
                {
                    case 1:
                        return "Bajo";
                    case 2:
                        return "Medio";
                    case 3:
                        return "Alto";
                    default:
                        return "Bajo";
                }
            }
        }
        public int? IdPadre { get; set; }
        public int? IdHermano { get; set; }
        public int IdEstado { get; set; }
        public string DescripcionEstado { get; set; }
        public Identifier Estado
        {
            get
            {
                return new Identifier
                {
                    id = IdEstado,
                    descripcion = DescripcionEstado
                };
            }
        }
        public IEnumerable<ActividadCotizacionModel> Hijo { get; set; }
    }

    public class TipoTrabajadorModel
    {
        public int IdTipoTrabajadorCotizacion { get; set; }
        public int IdCotizacion { get; set; }
        public int IdTipoTrabajador { get; set; }
        public string Descripcion { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }
    }
}
