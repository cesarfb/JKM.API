using System;
using System.Collections.Generic;

namespace JKM.PERSISTENCE.Repository.Proyecto
{
    public class ProyectoModel
    {
        public int IdProyecto { get; set; }
        public string NombreProyecto { get; set; }
        public string Descripcion { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public int IdEstado { get; set; }
        public decimal Precio { get; set; }

        public void RegisterProyecto(DateTime? fechaInicio = null, DateTime? fechaFin = null,
            string nombreProyecto = "", string descripcion = "")
        {
            NombreProyecto = nombreProyecto;
            Descripcion = descripcion;
            FechaInicio = fechaInicio;
            FechaFin = fechaFin;
        }

        public void UpdateProyecto(int idProyecto = 0, DateTime? fechaInicio = null, DateTime? fechaFin = null,
            string nombreProyecto = "", string descripcion = "", int idEstado = 0, decimal precio = 0)
        {
            IdProyecto = idProyecto;
            NombreProyecto = nombreProyecto;
            Descripcion = descripcion;
            FechaInicio = fechaInicio;
            FechaFin = fechaFin;
            IdEstado = idEstado;
            Precio = precio;
        }
    }

    public class ActividadProyectoModel
    {
        public int IdProyecto { get; set; }
        public int IdActividad { get; set; }
        public string Descripcion { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public int Peso { get; set; }
        public int? IdPadre { get; set; }
        public int? IdHermano { get; set; }
        public IEnumerable<ActividadProyectoModel> Hijo { get; set; }

        public void UpdateActividad(string descripcion = "", int peso = 0, int? idPadre = 0,
            int? idHermano = 0, int idActividad = 0, DateTime? fechaInicio = null, DateTime? fechaFin = null)
        {
            IdActividad = idActividad;
            Descripcion = descripcion;
            Peso = peso;
            IdPadre = idPadre;
            IdHermano = idHermano;
            FechaInicio = fechaInicio;
            FechaFin = fechaFin;
        }
    }
}
