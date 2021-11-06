using Dapper;
using JKM.APPLICATION.Aggregates;
using JKM.UTILITY.Utils;
using MediatR;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace JKM.APPLICATION.Queries.Pedido.GetPedido
{
    public class GetPedidoQueryHandler : IRequestHandler<GetPedidoQuery, PaginadoResponse<PedidoModelPaginado>>
    {
        private readonly IDbConnection _conexion;
        public GetPedidoQueryHandler(IDbConnection conexion)
        {
            _conexion = conexion;
        }

        public async Task<PaginadoResponse<PedidoModelPaginado>> Handle(GetPedidoQuery request, CancellationToken cancellationToken)
        {
            string sql = $@"SELECT COUNT(1) FROM Pedido;";

            sql += $@"SELECT P.idPedido, P.fechaRegistro, P.fechaEntrega, 
					  EP.idEstado, EP.descripcion,
					  V.precio, C.solicitante, C.email
					  FROM Pedido P
                      INNER JOIN EstadoPedido EP ON EP.idEstado = P.idEstado
                      INNER JOIN Venta V ON V.idVenta = P.idVenta
                      INNER JOIN Cotizacion C ON C.idCotizacion = V.idCotizacion
					  ORDER BY P.idPedido DESC;";

            using (IDbConnection connection = _conexion)
            {
                try
                {
                    PaginadoResponse<PedidoModelPaginado> newPaginado = new PaginadoResponse<PedidoModelPaginado>();

                    connection.Open();
                    using (var multi = await connection.QueryMultipleAsync(sql))
                    {
                        newPaginado.TotalRows = multi.ReadFirst<int>();
                        newPaginado.Data = multi.Read<PedidoModel>().AsList();
                    }
                    connection.Close();

                    if (newPaginado.Data.AsList().Count == 0) throw new ArgumentNullException();

                    return newPaginado;
                }
                catch (SqlException err)
                {
                    throw err;
                }
            }
        }
    }
}
