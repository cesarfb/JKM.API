using Dapper;
using JKM.UTILITY.Utils;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Transactions;
using static Dapper.SqlMapper;

namespace JKM.PERSISTENCE.Repository.Trabajador
{
    public class TrabajadorRepository : ITrabajadorRepository
    {
        private readonly IDbConnection _conexion;

        public TrabajadorRepository(IDbConnection conexion)
        {
            _conexion = conexion;
        }

        public async Task<ResponseModel> RegisterTrabajador(TrabajadorModel model)
        {
            string sql = $@"SELECT ISNULL(idTipoTrabajador,0)
                            FROM TipoTrabajador
                            WHERE idTipoTrabajador = @IdTipo";

            using (TransactionScope trans = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            using (IDbConnection connection = _conexion)
            {
                try
                {
                    connection.Open();

                    int existTipo = await connection.QueryFirstAsync<int>(sql, model);

                    if (existTipo <= 0)
                        Handlers.ExceptionClose(connection, "No se encontró el estado");

                    string insert = $@"INSERT INTO Trabajador
                                       (nombre, apellidoPaterno, apellidoMaterno, fechaNacimiento, idTipoTrabajador, idEstado)
                                       VALUES
                                       (@Nombre, @ApellidoPaterno, @ApellidoMaterno, @FechaNacimiento, @IdTipo, @IdEstado)";

                    int hasInsert = await connection.ExecuteAsync(insert, model);

                    if (hasInsert <= 0)
                        Handlers.ExceptionClose(connection, "Error al registrar el trabajador");

                    return Handlers.CloseConnection(connection, trans, "Se registró el trabajador");
                }
                catch (SqlException err)
                {
                    throw err;
                }
            }
        }

        public async Task<ResponseModel> UpdateTrabajador(TrabajadorModel model)
        {
            string sql = $@"SELECT ISNULL(idTrabajador,0)
                            FROM Trabajador
                            WHERE idTrabajador = @IdTrabajador;";

            sql += $@"SELECT ISNULL(idTipoTrabajador,0)
                     FROM TipoTrabajador
                     WHERE idTipoTrabajador = @IdTipo;";

            sql += $@"SELECT ISNULL(idEstado,0)
                     FROM EstadoTrabajador
                     WHERE idEstado = @IdEstado;";

            using (TransactionScope trans = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            using (IDbConnection connection = _conexion)
            {
                try
                {
                    connection.Open();

                    using (GridReader multi = await connection.QueryMultipleAsync(sql, model))
                    {
                        int existTrabajador = multi.ReadFirst<int>();
                        int existTipo = multi.ReadFirst<int>();
                        int existEstado = multi.ReadFirst<int>();

                        if (existTrabajador <= 0)
                            Handlers.ExceptionClose(connection, "No se encontró al trabajador");

                        if (existTipo <= 0)
                            Handlers.ExceptionClose(connection, "No se encontró el tipo de trabajador");

                        if (existEstado <= 0)
                            Handlers.ExceptionClose(connection, "No se encontró el estado");
                    }

                    string update = $@"UPDATE Trabajador
                                        SET nombre = @Nombre,
	                                        apellidoPaterno = @ApellidoPaterno,
	                                        apellidoMaterno = @ApellidoMaterno,
	                                        fechaNacimiento = @FechaNacimiento,
	                                        idTipoTrabajador = @IdTipo,
	                                        idEstado = @IdEstado
                                        WHERE idTrabajador = @IdTrabajador";

                    int hasUpdate = await connection.ExecuteAsync(update, model);

                    if (hasUpdate <= 0)
                        Handlers.ExceptionClose(connection, "Error al actualizar el trabajador");

                    return Handlers.CloseConnection(connection, trans, "Se actualizó el trabajador");
                }
                catch (SqlException err)
                {
                    throw err;
                }
            }
        }

        public async Task<ResponseModel> DeleteTrabajador(int idTrabajador)
        {
            string sql = $@"SELECT COUNT(1)
                            FROM ProyectoTrabajador
                            WHERE idTrabajador = {idTrabajador};";

            using (TransactionScope trans = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            using (IDbConnection connection = _conexion)
            {
                try
                {
                    connection.Open();

                    int bussyWorker = await connection.QueryFirstOrDefaultAsync<int>(sql);

                    if (bussyWorker > 0)
                        Handlers.ExceptionClose(connection, "El trabajador se encuentra vinculado a un proyecto actualmente.");

                    string delete = $@"DELETE
                                       FROM Trabajador
                                       WHERE idTrabajador = {idTrabajador};";

                    int hasUpdate = await connection.ExecuteAsync(delete);

                    if (hasUpdate <= 0)
                        Handlers.ExceptionClose(connection, "Error al eliminar el trabajador");

                    return Handlers.CloseConnection(connection, trans, "Se eliminó el trabajador");
                }
                catch (SqlException err)
                {
                    throw err;
                }
            }
        }

        public async Task<ResponseModel> RegisterTipoTrabajador(TipoTrabajadorProyectoModel model)
        {
            string sql = $@"SELECT COUNT(1)
                            FROM TipoTrabajador
                            WHERE nombre = @Nombre";

            using (TransactionScope trans = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            using (IDbConnection connection = _conexion)
            {
                try
                {
                    connection.Open();

                    int existTipo = await connection.QueryFirstAsync<int>(sql, model);

                    if (existTipo > 0)
                        Handlers.ExceptionClose(connection, "El nombre ingresado ya se en cuentra en uso");

                    string insert = $@"INSERT INTO TipoTrabajador
                                       (nombre, descripcion, precioReferencial)
                                       VALUES
                                       (@Nombre, @Descripcion, @PrecioReferencial)";

                    int hasInsert = await connection.ExecuteAsync(insert, model);

                    if (hasInsert <= 0)
                        Handlers.ExceptionClose(connection, "Error al registrar el tipo de trabajador");

                    return Handlers.CloseConnection(connection, trans, "Se registró el tipo de trabajador");
                }
                catch (SqlException err)
                {
                    throw err;
                }
            }
        }

        public async Task<ResponseModel> UpdateTipoTrabajador(TipoTrabajadorProyectoModel model)
        {
            string sql =  $@"SELECT COUNT(1)
                             FROM TipoTrabajador
                             WHERE idTipoTrabajador = @IdTipoTrabajador;";
            sql += $@"SELECT COUNT(1)
                      FROM TipoTrabajador
                      WHERE nombre = @Nombre
                      AND idTipoTrabajador <> @IdTipoTrabajador;";

            using (TransactionScope trans = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            using (IDbConnection connection = _conexion)
            {
                try
                {
                    connection.Open();

                    using (GridReader multi = await connection.QueryMultipleAsync(sql, model))
                    {
                        int existTipo = multi.ReadFirst<int>();
                        int existEstado = multi.ReadFirst<int>();

                        if (existTipo <= 0)
                            Handlers.ExceptionClose(connection, "No se encontró el tipo de trabajador");

                        if (existEstado > 0)
                            Handlers.ExceptionClose(connection, "El nombre ingresado ya se en cuentra en uso");
                    }

                    string update = $@"UPDATE TipoTrabajador
                                        SET nombre = @Nombre,
	                                        descripcion = @Descripcion,
	                                        precioReferencial = @PrecioReferencial
                                        WHERE idTipoTrabajador = @IdTipoTrabajador";

                    int hasUpdate = await connection.ExecuteAsync(update, model);

                    if (hasUpdate <= 0)
                        Handlers.ExceptionClose(connection, "Error al actualizar el tipo trabajador");

                    return Handlers.CloseConnection(connection, trans, "Se actualizó el tipo de trabajador");
                }
                catch (SqlException err)
                {
                    throw err;
                }
            }
        }
    }
}
