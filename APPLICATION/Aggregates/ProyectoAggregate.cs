using JKM.UTILITY.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace JKM.APPLICATION.Aggregates
{
    public class ActividadProyectoModel
    {
        public int IdActividad { get; set; }
        public string Descripcion { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
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
                        return "Default";
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
        public IEnumerable<ActividadProyectoModel> Hijo { get; set; }
    }

    public class ProyectoModel
    {
        public int IdProyecto { get; set; }
        public int? IdCotizacion { get; set; }
        public string NombreProyecto { get; set; }
        public string Descripcion { get; set; }
        public string RazonSocial { get; set; }
        public string Ruc { get; set; }
        public int? IdCliente { get; set; }
        public decimal? Precio { get; set; }
        private DateTime? FechaInicio { get; set; }
        [JsonProperty("fechaInicio")]
        public string FechaInicioString
        {
            get
            {
                return FechaInicio?.ToString("yyyy-MM-dd");
            }
        }
        private DateTime? FechaFin { get; set; }
        [JsonProperty("fechaFin")]
        public string FechaFinString
        {
            get
            {
                return FechaFin?.ToString("yyyy-MM-dd");
            }
        }
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
        private int IdEstado { get; set; }
        private string DescripcionEstado { get; set; }
    }

    public class TrabajadorProyectoModel
    {
        public int IdTrabajador { get; set; }
        private int IdTipoTrabajador { get; set; }
        private string DescripcionTipo { get; set; }
        public Identifier Tipo
        {
            get
            {
                return new Identifier
                {
                    id = IdTipoTrabajador,
                    descripcion = DescripcionTipo
                };
            }
        }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        private DateTime? FechaNacimiento { get; set; }
        [JsonProperty("fechaNacimiento")]
        public string FechaNacimientoString
        {
            get
            {
                return FechaNacimiento?.ToString("yyyy-MM-dd");
            }
        }
    }
}
