using JKM.UTILITY.Utils;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Transactions;
using static Dapper.SqlMapper;

namespace JKM.PERSISTENCE.Repository.Pedido
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly IDbConnection _conexion;

        public PedidoRepository(IDbConnection conexion)
        {
            _conexion = conexion;
        }

        public async Task<ResponseModel> UpdateEstado(int idPedido, int idEstado)
        {
            string sql = $@"SELECT COUNT(1) 
							FROM Pedido 
							WHERE idPedido = {idPedido};";
            sql += $@"SELECT COUNT(1) 
					  FROM EstadoPedido
					  WHERE idEstado= {idEstado};";

            using (TransactionScope trans = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            using (IDbConnection connection = _conexion)
            {
                try
                {
                    connection.Open();

                    using (GridReader multi = await connection.QueryMultipleAsync(sql))
                    {
                        int existPedido = multi.ReadFirst<int>();
                        int existEstado = multi.ReadFirst<int>();

                        if (existPedido <= 0)
                            Handlers.ExceptionClose(connection, "No se encontró el proyecto");

                        if (existEstado <= 0)
                            Handlers.ExceptionClose(connection, "No se encontró el estado");
                    }

                    string update = $@"UPDATE Pedido
			                               SET idEstado = {idEstado}
			                               WHERE idPedido = {idPedido}";

                    int hasUpdate = await connection.ExecuteAsync(update);

                    if (hasUpdate <= 0)
                        Handlers.ExceptionClose(connection, "Error al actualizar el pedido");

                    return Handlers.CloseConnection(connection, trans, "Se actualizó el pedido");
                }
                catch (SqlException err)
                {
                    throw err;
                }
            }
        }

        public async Task<ResponseModel> UpdateFechaRegistro(int idPedido, DateTime fechaEntrega)
        {
            string sql = $@"SELECT COUNT(1) 
							FROM Pedido 
							WHERE idPedido = {idPedido};";

            using (TransactionScope trans = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            using (IDbConnection connection = _conexion)
            {
                try
                {
                    connection.Open();

                    int exist = await connection.QueryFirstAsync<int>(sql);

                    if (exist == 0)
                        Handlers.ExceptionClose(connection, "No se encontró el pedido");

                    string update = $@"UPDATE Pedido
			                               SET fechaEntrega = @FechaEntrega
			                               WHERE idPedido = {idPedido}";

                    int hasUpdate = await connection.ExecuteAsync(update, new { FechaEntrega  = fechaEntrega });

                    if (hasUpdate <= 0)
                        Handlers.ExceptionClose(connection, "Error al actualizar el pedido");

                    return Handlers.CloseConnection(connection, trans, "Se actualizó el pedido");
                }
                catch (SqlException err)
                {
                    throw err;
                }
            }
        }
    }
}
