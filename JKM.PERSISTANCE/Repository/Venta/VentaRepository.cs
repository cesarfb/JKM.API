using Dapper;
using JKM.PERSISTENCE.Repository.Proyecto;
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

        public async Task<ResponseModel> RegisterVenta(VentaModel ventaModel, ProyectoModel proyectoModel)
        {
            using (TransactionScope trans = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            using (IDbConnection connection = _conexion)
            {
                try
                {
                    connection.Open();

                    //Registro Cuota
                    string insert = $@"INSERT INTO DetalleVenta
                                        	(precio, fecha, idVenta)
                                       VALUES 
                                        	(@PagoParcial, @FechaCuota, @IdVenta)";

                    int hasInsert = await connection.ExecuteAsync(insert, ventaModel);

                    if (hasInsert <= 0)
                        Handlers.ExceptionClose(connection, "Error al registrar la Cuota");

                    //Comprueba si es segunda cuota o primera cuota
                    string sql = $@"SELECT 
						                idTipo
                                    FROM Venta 
					                WHERE idVenta = {ventaModel.IdVenta};";

                    int idTipoVenta = await connection.QueryFirstAsync<int>(sql);

                    string sqlCount = $@"SELECT 
						                    COUNT(1)
                                        FROM DetalleVenta 
					                    WHERE idVenta = {ventaModel.IdVenta};";

                    int countCuota = await connection.QueryFirstAsync<int>(sqlCount);

                    if (countCuota == 1)
                    {
                        //Tipo Proyecto
                        if (idTipoVenta == 1)
                        {
                            string insertProyecto = $@"INSERT INTO Proyecto
                                        	            (nombre, fechaInicio, descripcion, idEstado)
                                                   VALUES 
                                        	            (@NombreProyecto, GETDATE(), @Descripcion, 1);

                                                    SELECT ISNULL(@@IDENTITY,-1);";

                            decimal hasInsertProyecto = (decimal)await connection.ExecuteScalarAsync(insertProyecto, proyectoModel);

                            if (hasInsertProyecto <= 0)
                                Handlers.ExceptionClose(connection, "Error al registrar la Proyecto");


                            string insertProyectoVenta = $@"INSERT INTO ProyectoVenta
                                        	                (idVenta, idProyecto)
                                                        VALUES 
                                        	                (@idVenta,  @idProyecto);";

                            int hasInsertProyectoVenta = await connection.ExecuteAsync(insertProyectoVenta, new { idVenta = ventaModel.IdVenta, idProyecto = hasInsertProyecto });

                            if (hasInsertProyectoVenta <= 0)
                                Handlers.ExceptionClose(connection, "Error al registrar la Proyecto Venta");
                        }

                        //Tipo Pedido
                        else if (idTipoVenta == 2)
                        {

                            if (proyectoModel.IdProyecto != null)
                            {
                                //Insertar Tabla Pedido
                                string insertPedido = $@"INSERT INTO Pedido
                                        	            (fechaRegistro, idVenta)
                                                   VALUES 
                                        	            (GETDATE(), @IdVenta);";

                                int hasInsertPedido = await connection.ExecuteAsync(insertPedido, ventaModel);

                                if (hasInsertPedido <= 0)
                                    Handlers.ExceptionClose(connection, "Error al registrar el Pedido");

                                //Insertar Tabla Proyecto Venta
                                string insertProyectoVenta = $@"INSERT INTO ProyectoVenta
                                        	                (idVenta, idProyecto)
                                                        VALUES 
                                        	                (@idVenta,  @idProyecto);";

                                int hasInsertProyectoVenta = await connection.ExecuteAsync(insertProyectoVenta, new { idVenta = ventaModel.IdVenta, idProyecto = proyectoModel.IdProyecto });

                                if (hasInsertProyectoVenta <= 0)
                                    Handlers.ExceptionClose(connection, "Error al registrar la Proyecto Venta");
                            }
                            else
                            {
                                //Insertar Tabla Pedido
                                string insertPedido = $@"INSERT INTO Pedido
                                        	            (fechaRegistro, idVenta, idEstado)
                                                   VALUES 
                                        	            (GETDATE(), @IdVenta, 1);";

                                int hasInsertPedido = await connection.ExecuteAsync(insertPedido, ventaModel);

                                if (hasInsertPedido <= 0)
                                    Handlers.ExceptionClose(connection, "Error al registrar el Pedido");
                            }

                        }
                    }


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
