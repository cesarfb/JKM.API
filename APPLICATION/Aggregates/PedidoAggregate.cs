using JKM.UTILITY.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace JKM.APPLICATION.Aggregates
{
    public class PedidoModelPaginado
    {
        public int IdPedido { get; set; }
        public DateTime FechaRegistro { get; set; }
        [JsonProperty("fechaRegistro")]
        public string FechaRegistroString
        {
            get
            {
                return FechaRegistro.ToString("yyyy-MM-dd");
            }
        }
        public DateTime FechaEntrega { get; set; }
        [JsonProperty("fechaEntrega")]
        public string FechaEntregaString
        {
            get
            {
                return FechaEntrega.ToString("yyyy-MM-dd");
            }
        }
        public int IdEstado { get; set; }
        public string Descripcion { get; set; }
        public Identifier Estado
        {
            get
            {
                return new Identifier
                {
                    descripcion = Descripcion,
                    id = IdEstado
                };
            }
        }
    }
    public class PedidoModel : PedidoModelPaginado
    {
        public int Precio { get; set; }
        public int Solicitante { get; set; }
        public int Email { get; set; }
        public int Cantidad { get; set; }
        public float PrecioProd { get; set; }
        public int IdProducto { get; set; }
        public int Codigo { get; set; }
        public int Nombre { get; set; }
        public int Imagen { get; set; }
    }
    public class PedidoModelFormat : PedidoModelPaginado
    {
        public int Precio { get; set; }
        public int Solicitante { get; set; }
        public int Email { get; set; }
        public List<Pedidos> Pedidos { get; set; }
    }

    public class Pedidos
    {
        public int Cantidad { get; set; }
        public float PrecioProd { get; set; }
        public int IdProducto { get; set; }
        public int Codigo { get; set; }
        public int Nombre { get; set; }
        public int Imagen { get; set; }
    }
}