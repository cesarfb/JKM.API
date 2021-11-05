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
        public int IdTipoCotizacion { get; set; }

        public void RegisterCotizacion(string solicitante = "", DateTime? fechaSolicitud = null,
            string descripcion = "", string email = "", int idCliente = 0, double precioCotizacion = 0, int idTipoCotizacion = 0)
        {
            Solicitante = solicitante;
            Descripcion = descripcion;
            FechaSolicitud = fechaSolicitud;
            Email = email;
            IdCliente = idCliente;
            PrecioCotizacion = precioCotizacion;
            IdTipoCotizacion = idTipoCotizacion;
        }

        public void UpdateCotizacion(string solicitante = "", DateTime? fechaSolicitud = null,
            string descripcion = "", string email = "", int idCliente = 0, int idCotizacion = 0, double precioCotizacion = 0.00,
            int idTipoCotizacion = 0)
        {
            IdCotizacion = idCotizacion;
            Solicitante = solicitante;
            Descripcion = descripcion;
            FechaSolicitud = fechaSolicitud;
            Email = email;
            IdCliente = idCliente;
            PrecioCotizacion = precioCotizacion;
            IdTipoCotizacion = idTipoCotizacion;
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
            int? idHermano = 0, int idActividad = 0, int idCotizacion = 0)
        {
            IdCotizacion = idCotizacion;
            Descripcion = descripcion;
            Peso = peso;
            IdPadre = idPadre;
            IdHermano = idHermano;
            IdActividad = idActividad;
        }
    }

    public class DetalleOrdenCotizacionModel
    {
        public int IdCotizacion { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }
        public int IdDetalleOrden { get; set; }
        public int IdProducto { get; set; }
        public int IdTipoDetalleOrden { get; set; }
        public void RegisterDetalleOrden(decimal precio = 0, int cantidad = 0,
            int idCotizacion = 0, int idProducto = 0, int idTipoDetalleOrden = 1)
        {
            Precio = precio;
            Cantidad = cantidad;
            IdCotizacion = idCotizacion;
            IdProducto = idProducto;
            IdTipoDetalleOrden = idTipoDetalleOrden;

        }
        public void UpdateDetalleOrden(decimal precio = 0, int cantidad = 0,
            int idDetalleOrden = 0, int idCotizacion = 0, int idProducto = 0, int idTipoDetalleOrden = 0)
        {
            Precio = precio;
            Cantidad = cantidad;
            IdCotizacion = idCotizacion;
            IdProducto = idProducto;
            IdTipoDetalleOrden = idTipoDetalleOrden;
            IdDetalleOrden = idDetalleOrden;
        }
    }

}
