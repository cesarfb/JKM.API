using JKM.UTILITY.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace JKM.APPLICATION.Aggregates
{
    public class PedidoModelPaginado
    {
        public int IdPedido { get; set; }
        private DateTime FechaRegistro { get; set; }
        [JsonProperty("fechaRegistro")]
        public string FechaRegistroString
        {
            get
            {
                return FechaRegistro.ToString("yyyy-MM-dd");
            }
        }
        private DateTime FechaEntrega { get; set; }
        [JsonProperty("fechaEntrega")]
        public string FechaEntregaString
        {
            get
            {
                return FechaEntrega.ToString("yyyy-MM-dd");
            }
        }
        private int IdEstado { get; set; }
        private string Descripcion { get; set; }
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
        public string Solicitante { get; set; }
        public string Email { get; set; }
        public int Cantidad { get; set; }
        public float PrecioProd { get; set; }
        public int IdProducto { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Imagen { get; set; }
    }
    public class PedidoModelFormat : PedidoModelPaginado
    {
        public int Precio { get; set; }
        public string Solicitante { get; set; }
        public string Email { get; set; }
        public List<Pedidos> Pedidos { get; set; }
    }

    public class Pedidos
    {
        public int Cantidad { get; set; }
        public float PrecioProd { get; set; }
        public int IdProducto { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Imagen { get; set; }
    }
}