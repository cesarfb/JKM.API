using Dapper;
using JKM.UTILITY.Utils;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Transactions;
using static Dapper.SqlMapper;

namespace JKM.PERSISTENCE.Repository.Cliente
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly IDbConnection _conexion;

        public ClienteRepository(IDbConnection conexion)
        {
            _conexion = conexion;
        }

        public async Task<ResponseModel> RegisterCliente(ClienteModel clienteModel)
        {
            string sql = $@"SELECT COUNT(1) 
                            FROM Cliente
                            WHERE RUC = '{clienteModel.RUC.ToUpper()}';";

            using (TransactionScope trans = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            using (IDbConnection connection = _conexion)
            {
                try
                {
                    connection.Open();

                    using (GridReader multi = await connection.QueryMultipleAsync(sql))
                    {
                        int clienteRepeated = multi.ReadFirst<int>();

                        if (clienteRepeated > 0)
                            Handlers.ExceptionClose(connection, "Ya existe un cliente con el RUC indicado");
                    }

                    string insert = $@"INSERT INTO Cliente
		                                        (razonSocial, ruc, telefono)
		                                        VALUES
		                                        (TRIM(@RazonSocial), @RUC, @Telefono);";

                    int hasInsert = await connection.ExecuteAsync(insert, clienteModel);

                    if (hasInsert <= 0)
                        Handlers.ExceptionClose(connection, "Ocurrió un error al insertar el cliente");

                    return Handlers.CloseConnection(connection, trans, "Registro exitoso");
                }
                catch (SqlException err)
                {
                    throw err;
                }
            }
        }

        public async Task<ResponseModel> UpdateCliente(ClienteModel clienteModel)
        {
            string sql = $@"SELECT COUNT(1) 
                            FROM Cliente
                            WHERE RUC = '{clienteModel.RUC.ToUpper()}' AND idCliente != {clienteModel.IdCliente};";

            using (TransactionScope trans = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            using (IDbConnection connection = _conexion)
            {
                try
                {
                    connection.Open();

                    using (GridReader multi = await connection.QueryMultipleAsync(sql))
                    {
                        int clienteRepeated = multi.ReadFirst<int>();

                        if (clienteRepeated > 0)
                            Handlers.ExceptionClose(connection, "Ya existe un cliente con el RUC indicado");
                    }

                    string update = $@"UPDATE Cliente SET
		                                        razonSocial = TRIM(@RazonSocial),
                                                ruc = @RUC,
                                                telefono = @Telefono
                                        WHERE idCliente = @IdCliente";

                    int hasUpdated = await connection.ExecuteAsync(update, clienteModel);

                    if (hasUpdated <= 0)
                        Handlers.ExceptionClose(connection, "Ocurrió un error al actualizar el cliente");

                    return Handlers.CloseConnection(connection, trans, "Actualizacion exitosa");
                }
                catch (SqlException err)
                {
                    throw err;
                }
            }
        }
    }
}
