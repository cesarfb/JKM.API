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
        [JsonProperty("fechaSolicitud")]
        public string fechaSolicitudString
        {
            get
            {
                return fechaSolicitud.ToString("yyyy-MM-dd");
            }
        }
        public string email { get; set; }
        public int idCliente { get; set; }
        public string razonSocial { get; set; }
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

        public Identifier tipoCotizacion
        {
            get
            {
                return new Identifier
                {
                    id = idTipoCotizacion,
                    descripcion = descripcionTipoCotizacion
                };
            }
        }
        private int idEstado { get; set; }
        private string descripcionEstado { get; set; }
        private int idTipoCotizacion { get; set; }
        private string descripcionTipoCotizacion { get; set; }
        public double precioCotizacion { get; set; }
        public bool canCotizar { get; set; }
        public bool canDelete { get; set; }
        public bool canEdit { get; set; }
    }
    public class ActividadCotizacionModel
    {
        [JsonProperty("IdCotizacion")]
        public int IdCotizacion { get; set; }
        [JsonProperty("IdActividad")]
        public int IdActividad { get; set; }
        [JsonProperty("Descripcion")]
        public string Descripcion { get; set; }
        [JsonProperty("Peso")]
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
        [JsonProperty("IdPadre")]
        public int? IdPadre { get; set; }
        [JsonProperty("IdHermano")]
        public int? IdHermano { get; set; }
        [JsonProperty("IdEstado")]
        public int IdEstado { get; set; }
        public string DescripcionEstado { get; set; }
        [JsonProperty("Profundidad")]
        public int Profundidad { get; set; }
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
    }

    public class ActividadCotizancionTreeNode
    {
        public ActividadCotizacionModel data { get; set; }
        public IEnumerable<ActividadCotizancionTreeNode>? children { get; set; }
    }
    public class TipoTrabajadorModel
    {
        public int idCotizacion { get; set; }
        public int idTipoTrabajador { get; set; }
        public string descripcion { get; set; }
        public int cantidad { get; set; }
        public decimal precio { get; set; }
    }

    public class DetalleOrdenModel
    {
        public int idDetalleOrden { get; set; }
        public int idCotizacion { get; set; }
        public int idProducto { get; set; }
        public string codigoProducto { get; set; }
        public string nombreProducto { get; set; }
        public string imagen { get; set; }
        public int cantidad { get; set; }
        public decimal precio { get; set; }
    }
}