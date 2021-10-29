using Dapper;
using JKM.UTILITY.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using static Dapper.SqlMapper;

namespace JKM.PERSISTENCE.Repository.Venta
{
    public class VentaRepository : IVentaRepository
    {
        private readonly IDbConnection _conexion;

        public VentaRepository(IDbConnection conexion)
        {
            _conexion = conexion;
        }

        public async Task<ResponseModel> RegisterVenta(VentaModel ventaModel)
        {
            using (TransactionScope trans = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            using (IDbConnection connection = _conexion)
            {
                try
                {
                    connection.Open();

                    string insert = $@"INSERT INTO DetalleVenta
                                        	(precio, fecha, idVenta)
                                        VALUES 
                                        	(@PagoParcial, @FechaCuota, @IdVenta)";

                    int hasInsert = await connection.ExecuteAsync(insert, ventaModel);
                    if (hasInsert <= 0)
                        Handlers.ExceptionClose(connection, "Error al registrar la venta");

                    return Handlers.CloseConnection(connection, trans, "Se registró la venta");
                }
                catch (SqlException err)
                {
                    throw err;
                }
            }


        }

        public async Task<ResponseModel> DeleteVenta(int idVenta)
        {
            string sql = $@"SELECT COUNT(1) 
						    FROM Venta 
						    WHERE idVenta = {idVenta};";

            sql += $@"SELECT COUNT(1)
                        FROM DetalleVenta
                        WHERE idVenta = {idVenta};";

            using (TransactionScope trans = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            using (IDbConnection connection = _conexion)
            {
                try
                {
                    connection.Open();

                    using (GridReader multi = await connection.QueryMultipleAsync(sql))
                    {

                        int exist = multi.ReadFirst<int>();
                        int hasChild = multi.ReadFirst<int>();

                        if (exist <= 0)
                            Handlers.ExceptionClose(connection, "No se encontro la Venta");

                        if (hasChild > 0)
                            Handlers.ExceptionClose(connection, "Elimine las cuotas para continuar");

                    }

                    string delete = $@"DELETE FROM Venta 
                                        WHERE idVenta = {idVenta}";

                    int hasDelete = await connection.ExecuteAsync(delete);

                    if (hasDelete <= 0)
                        Handlers.ExceptionClose(connection, "Ocurrio un error al eliminar la venta");

                    return Handlers.CloseConnection(connection, trans, "Se elimino la venta");
                }
                catch (SqlException err)
                {
                    throw err;
                }
            }
        }

    }
}
