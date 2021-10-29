using System;
using System.Collections.Generic;

namespace JKM.PERSISTENCE.Repository.Cotizacion
{
    public class CotizacionModel
    {
        public int IdCotizacion { get; set; }
        public string Solicitante { get; set; }
        public string Descripcion { get; set; }
        public DateTime? FechaSolicitud { get; set; }
        public string Email { get; set; }
        public int IdCliente { get; set; }
        public int IdEstado { get; set; }
        public double PrecioCotizacion { get; set; }

        public void RegisterCotizacion(string solicitante = "", DateTime? fechaSolicitud = null,
            string descripcion = "", string email = "", int idCliente = 0, double precioCotizacion = 0)
        {
            Solicitante = solicitante;
            Descripcion = descripcion;
            FechaSolicitud = fechaSolicitud;
            Email = email;
            IdCliente = idCliente;
            PrecioCotizacion = precioCotizacion;
        }

        public void UpdateCotizacion(string solicitante = "", DateTime? fechaSolicitud = null,
            string descripcion = "", string email = "", int idCliente = 0, int idCotizacion = 0, double precioCotizacion = 0.00)
        {
            IdCotizacion = idCotizacion;
            Solicitante = solicitante;
            Descripcion = descripcion;
            FechaSolicitud = fechaSolicitud;
            Email = email;
            IdCliente = idCliente;
            PrecioCotizacion = precioCotizacion;
        }

    }

    public class TipoTrabajadorModel
    {
        public int IdCotizacion { get; set; }
        public int IdTipoTrabajador { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }
        public void RegisterTipoTrabajador(int idCotizacion, int idTipoTrabajador, int cantidad, decimal precio)
        {
            IdCotizacion = idCotizacion;
            IdTipoTrabajador = idTipoTrabajador;
            Cantidad = cantidad;
            Precio = precio;
        }
        public void UpdateTipoTrabajador(int idCotizacion, int idTipoTrabajador, int cantidad, decimal precio)
        {
            IdCotizacion = idCotizacion;
            IdTipoTrabajador = idTipoTrabajador;
            Cantidad = cantidad;
            Precio = precio;
        }
    }

    public class ActividadCotizacionModel
    {
        public int IdCotizacion { get; set; }
        public int IdActividad { get; set; }
        public string Descripcion { get; set; }
        public int Peso { get; set; }
        public int? IdPadre { get; set; }
        public int? IdHermano { get; set; }
        public IEnumerable<ActividadCotizacionModel> Hijo { get; set; }
        public void RegisterActividad(string descripcion = "", int peso = 0, int? idPadre = 0,
            int? idHermano = 0, int idCotizacion = 0)
        {
            Descripcion = descripcion;
            Peso = peso;
            IdPadre = idPadre;
            IdHermano = idHermano;
            IdCotizacion = idCotizacion;
        }
        public void UpdateActividad(string descripcion = "", int peso = 0, int? idPadre = 0,
            int? idHermano = 0, int idActividad = 0)
        {
            Descripcion = descripcion;
            Peso = peso;
            IdPadre = idPadre;
            IdHermano = idHermano;
            IdActividad = idActividad;
        }
    }

}
