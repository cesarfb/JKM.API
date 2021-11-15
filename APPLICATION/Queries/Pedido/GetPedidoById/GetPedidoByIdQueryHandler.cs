using Dapper;
using JKM.APPLICATION.Aggregates;
using MediatR;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;

namespace JKM.APPLICATION.Queries.Pedido.GetPedidoById
{
    public class GetPedidoByIdQueryHandler : IRequestHandler<GetPedidoByIdQuery, PedidoModelFormat>
    {
        private readonly IDbConnection _conexion;
        public GetPedidoByIdQueryHandler(IDbConnection conexion)
        {
            _conexion = conexion;
        }

        public async Task<PedidoModelFormat> Handle(GetPedidoByIdQuery request, CancellationToken cancellationToken)
        {
            string sql = $@"SELECT P.idPedido, P.fechaRegistro, P.fechaEntrega, P.codigo AS 'codigoOrden',
					  EP.idEstado, EP.descripcion,
					  V.precio as 'Precio', 
					  C.solicitante, C.email,
					  DO.cantidad, DO.precio as 'PrecioProd',
					  Prod.idProducto, Prod.codigo, Prod.nombre, Prod.imagen,
                      CLI.razonSocial as 'Cliente'
					  FROM Pedido P
                      INNER JOIN EstadoPedido EP ON EP.idEstado = P.idEstado
                      INNER JOIN Venta V ON V.idVenta = P.idVenta
                      INNER JOIN Cotizacion C ON C.idCotizacion = V.idCotizacion
                      INNER JOIN DetalleOrden DO ON DO.idCotizacion = C.idCotizacion
                      INNER JOIN Producto Prod ON Prod.idProducto = DO.idProducto
                      INNER JOIN Cliente CLI ON C.idCliente = CLI.idCliente
                      WHERE P.idPedido = {request.IdPedido}";

            using (IDbConnection connection = _conexion)
            {
                try
                {
                    connection.Open();

                    IEnumerable<PedidoModel> pedidoModel =
                        await connection.QueryAsync<PedidoModel>(sql);

                    connection.Close();

                    PedidoModelFormat pedido = new PedidoModelFormat();
                    pedido.IdPedido = pedidoModel.FirstOrDefault().IdPedido;
                    pedido.CodigoOrden = pedidoModel.FirstOrDefault().CodigoOrden;
                    pedido.Precio = pedidoModel.FirstOrDefault().Precio;
                    pedido.Solicitante = pedidoModel.FirstOrDefault().Solicitante;
                    pedido.Email = pedidoModel.FirstOrDefault().Email;
                    pedido.FechaRegistro = pedidoModel.FirstOrDefault().FechaRegistro;
                    pedido.FechaEntrega = pedidoModel.FirstOrDefault().FechaEntrega;
                    pedido.Cliente = pedidoModel.FirstOrDefault().Cliente;
                    pedido.Pedidos = (from p in pedidoModel
                                      select new Pedidos { 
                                          IdProducto = p.IdProducto,
                                          Cantidad = p.Cantidad,
                                          PrecioProd = p.PrecioProd,
                                          Codigo = p.Codigo,
                                          Nombre = p.Nombre,
                                          Imagen = p.Imagen,
                                      }).ToList();

                    if (pedido == null) throw new ArgumentNullException();

                    return pedido;
                }
                catch (SqlException err)
                {
                    throw err;
                }
            }
        }
    }
}