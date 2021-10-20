using JKM.UTILITY.Utils;
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
        public string fechaSolicitudString
        {
            get
            {
                return fechaSolicitud.ToString("dd-MM-yyyy");
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
        private int idEstado { get; set; }
        private string descripcionEstado { get; set; }
        public double precioCotizacion { get; set; }
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
