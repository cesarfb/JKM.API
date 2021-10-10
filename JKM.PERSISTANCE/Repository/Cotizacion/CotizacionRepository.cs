using Dapper;
using JKM.UTILITY.Utils;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Transactions;
using static Dapper.SqlMapper;

namespace JKM.PERSISTENCE.Repository.Cotizacion
{
    public class CotizacionRepository : ICotizacionRepository
    {
        private readonly IDbConnection _conexion;

        public CotizacionRepository(IDbConnection conexion)
        {
            _conexion = conexion;
        }

        public async Task<ResponseModel> AceptarCotizacion(int idCotizacion)
        {
            string sql = $@"SELECT COUNT(1) 
						    FROM Cotizacion 
						    WHERE idCotizacion = {idCotizacion} 
						    AND idEstado = 1;";

            using (TransactionScope trans = new TransactionScope())
            using (IDbConnection connection = _conexion)
            {
                try
                {
                    connection.Open();

                    int exist = await connection.QueryFirstAsync<int>(sql);

                    if (exist == 0)
                        Handlers.ExceptionClose(connection, "No se encontró la cotización");

                    string update = $@"UPDATE Cotizacion
		                                   SET idEstado = 2
		                                   WHERE idCotizacion = {idCotizacion};";

                    int hasUpdate = await connection.ExecuteAsync(update);

                    if (hasUpdate <= 0)
                        Handlers.ExceptionClose(connection, "Error al actualizar la cotización");

                    return Handlers.CloseConnection(connection, trans, "Se actualizó la cotización");
                }
                catch (SqlException err)
                {
                    throw err;
                }
            }
        }

        public async Task<ResponseModel> RechazarCotizacion(int idCotizacion)
        {
            string sql = $@"SELECT COUNT(1) 
						    FROM Cotizacion 
						    WHERE idCotizacion = { idCotizacion }
							    AND idEstado = 1;";

            using (TransactionScope trans = new TransactionScope())
            using (IDbConnection connection = _conexion)
            {
                try
                {
                    connection.Open();

                    int exist = await connection.QueryFirstAsync<int>(sql);

                    if (exist == 0)
                        Handlers.ExceptionClose(connection, "No se encontró la cotización");

                    string update = $@"UPDATE Cotizacion
		                                   SET idEstado = 3
		                                   WHERE idCotizacion = {idCotizacion};";

                    int hasUpdate = await connection.ExecuteAsync(update);

                    if (hasUpdate <= 0)
                        Handlers.ExceptionClose(connection, "Error al actualizar la cotización");

                    return Handlers.CloseConnection(connection, trans, "Se actualizó la cotización");
                }
                catch (SqlException err)
                {
                    throw err;
                }
            }
        }

        public async Task<ResponseModel> RegisterCotizacion(CotizacionModel cotizacionModel)
        {
            using (TransactionScope trans = new TransactionScope())
            using (IDbConnection connection = _conexion)
            {
                try
                {
                    connection.Open();

                    string insert = $@"INSERT INTO Cotizacion 
                                        (solicitante,descripcion,fechaSolicitud,email,empresa,idEstado)
		                                VALUES
		                                (@Solicitante,@Descripcion,@FechaSolicitud,@Email,@Empresa,1);";

                    int hasInsert = await connection.ExecuteAsync(insert, cotizacionModel);

                    if (hasInsert <= 0)
                        Handlers.ExceptionClose(connection, "Error al registrar la cotización");

                    return Handlers.CloseConnection(connection, trans, "Se registró la cotización");
                }
                catch (SqlException err)
                {
                    throw err;
                }
            }
        }

        public async Task<ResponseModel> UpdateCotizacion(CotizacionModel cotizacionModel)
        {
            string sql = $@"SELECT COUNT(*) 
							FROM EstadoCotizacion 
							WHERE idEstado = {cotizacionModel.IdEstado};";
            sql += $@"SELECT COUNT(*) 
					  FROM PrecioCotizacion 
					  WHERE idPrecioCotizacion = {cotizacionModel.IdPrecioCotizacion};";

            using (TransactionScope trans = new TransactionScope())
            using (IDbConnection connection = _conexion)
            {
                try
                {
                    connection.Open();

                    using (GridReader multi = await connection.QueryMultipleAsync(sql))
                    {
                        int existEstado = multi.ReadFirst<int>();
                        int hasPaid = multi.ReadFirst<int>();

                        if (existEstado <= 0)
                            Handlers.ExceptionClose(connection, "No se encontró el estado");

                        if (hasPaid == 0)
                        {
                            string insert = $@"INSERT INTO PrecioCotizacion
				                                (precioCotizacion,fecha,idCotizacion) 
				                                VALUES
				                                (@PrecioCotizacion,GETDATE(),@IdCotizacion);";

                            int hasInsert = await connection.ExecuteAsync(insert, cotizacionModel);

                            if (hasInsert <= 0)
                                Handlers.ExceptionClose(connection, "Error al registrar");

                        }
                        else if (hasPaid > 0)
                        {
                            string updatePrice = $@"UPDATE PrecioCotizacion
				                                       SET precioCotizacion = @PrecioCotizacion
				                                       WHERE idPrecioCotizacion = @IdPrecioCotizacion;";

                            int hasUpdatePrice = await connection.ExecuteAsync(updatePrice, cotizacionModel);

                            if (hasUpdatePrice <= 0)
                                Handlers.ExceptionClose(connection, "Error al actualizar");
                        }

                        string update = $@"UPDATE Cotizacion	
			                                    SET solicitante = @Solicitante,
				                                    descripcion = @Descripcion,
				                                    fechaSolicitud = @FechaSolicitud,
				                                    email = @Email,
				                                    empresa = @Empresa,
				                                    idEstado = @IdEstado
			                                    WHERE idCotizacion = @IdCotizacion;";

                        int hasUpdate = await connection.ExecuteAsync(update, cotizacionModel);

                        if (hasUpdate <= 0)
                            Handlers.ExceptionClose(connection, "Error al actualizar");

                        return Handlers.CloseConnection(connection, trans, "Actualización exitosa");
                    }
                }
                catch (SqlException err)
                {
                    throw err;
                }
            }
        }



        public async Task<ResponseModel> RegisterTrabajadorCotizacion(TipoTrabajadorModel trabajadorModel)
        {
            string sql = $@"SELECT COUNT(1) 
							FROM Cotizacion 
							WHERE idCotizacion = {trabajadorModel.IdCotizacion};";
            sql += $@"SELECT COUNT(1)
                      FROM TipoTrabajador
                      WHERE idTipoTrabajador = {trabajadorModel.IdTipoTrabajador};";

            using (TransactionScope trans = new TransactionScope())
            using (IDbConnection connection = _conexion)
            {
                try
                {
                    connection.Open();

                    using (GridReader multi = await connection.QueryMultipleAsync(sql))
                    {
                        int existCot = multi.ReadFirst<int>();
                        int existTipo = multi.ReadFirst<int>();

                        if (existCot <= 0)
                            Handlers.ExceptionClose(connection, "No se encontró la cotización solicitada");

                        if (existTipo <= 0)
                            Handlers.ExceptionClose(connection, "No se encontró el tipo de trabajador");
                    }

                    string insert = $@"INSERT INTO TipoTrabajadorCotizacion
		                                       (idCotizacion, idTipoTrabajador, precio, cantidad)
		                                       VALUES
		                                       (@IdCotizacion, @IdTipo, @Precio, @Cantidad);";

                    int hasInsert = await connection.ExecuteAsync(insert, trabajadorModel);

                    if (hasInsert <= 0)
                        Handlers.ExceptionClose(connection, "Ocurrió un error al insertar el tipo de trabajador");

                    return Handlers.CloseConnection(connection, trans, "Registro exitoso");
                }
                catch (SqlException err)
                {
                    throw err;
                }
            }
        }

        public async Task<ResponseModel> UpdateTrabajadorCotizacion(TipoTrabajadorModel trabajadorModel)
        {
            string sql = $@"SELECT COUNT(1) 
							FROM TipoTrabajadorCotizacion 
							WHERE idCotizacion = {trabajadorModel.IdCotizacion}
                                AND idTipoTrabajador = {trabajadorModel.IdTipoTrabajador};";

            sql += $@"SELECT COUNT(1) 
					  FROM TipoTrabajador 
				      WHERE idTipoTrabajador = {trabajadorModel.IdTipoTrabajador};";

            using (TransactionScope trans = new TransactionScope())
            using (IDbConnection connection = _conexion)
            {
                try
                {
                    connection.Open();

                    using (GridReader multi = await connection.QueryMultipleAsync(sql))
                    {
                        int existCot = multi.ReadFirst<int>();
                        int existTipo = multi.ReadFirst<int>();

                        if (existCot <= 0)
                            Handlers.ExceptionClose(connection, "No se encontró la cotización solicitada");

                        if (existTipo <= 0)
                            Handlers.ExceptionClose(connection, "No se encontró el tipo de trabajador");
                    }

                    string update = $@"UPDATE TipoTrabajadorCotizacion
		                                        SET precio = @Precio, 
			                                        cantidad = @Cantidad
		                                        WHERE idCotizacion = @IdCotizacion
                                                    AND idTipoTrabajador = @IdTipoTrabajador;";

                    int hasUpdate = await connection.ExecuteAsync(update, trabajadorModel);

                    if (hasUpdate <= 0)
                        Handlers.ExceptionClose(connection, "Error al actualizar");

                    return Handlers.CloseConnection(connection, trans, "Actualizacion exitosa");
                }
                catch (SqlException err)
                {
                    throw err;
                }
            }
        }

        public async Task<ResponseModel> DeleteTrabajadorCotizacion(int idCotizacion, int idTipo)
        {
            string sql = $@"SELECT COUNT(1) 
						    FROM TipoTrabajador 
						    WHERE idTipoTrabajador = {idTipo};";

            using (TransactionScope trans = new TransactionScope())
            using (IDbConnection connection = _conexion)
            {
                try
                {
                    connection.Open();

                    int exist = await connection.QueryFirstAsync<int>(sql);

                    if (exist <= 0)
                        Handlers.ExceptionClose(connection, "No se encontró el tipo de trabajador");

                    string delete = $@"DELETE FROM TipoTrabajadorCotizacion
		                                   WHERE idCotizacion = {idCotizacion}
                                                AND idTipoTrabajador = {idTipo};";

                    int hasUpdate = await connection.ExecuteAsync(delete);

                    if (hasUpdate <= 0)
                        Handlers.ExceptionClose(connection, "Ocurrió un error al eliminar el tipo de trabajador");

                    return Handlers.CloseConnection(connection, trans, "Se eliminó el tipo de trabajador");
                }
                catch (SqlException err)
                {
                    throw err;
                }
            }
        }

        public async Task<ResponseModel> RegisterActividadCotizacion(ActividadCotizacionModel actividadModel)
        {
            string sql = $@"SELECT COUNT(1) 
                            FROM Cotizacion 
                            WHERE idCotizacion = {actividadModel.IdCotizacion};";

            sql += $@"SELECT COUNT(1) 
					  FROM ActividadProyecto 
					  WHERE descripcion LIKE TRIM({actividadModel.Descripcion}) 
						AND idPadre = {actividadModel.IdPadre}
						AND idCotizacion = {actividadModel.IdCotizacion};";

            using (TransactionScope trans = new TransactionScope())
            using (IDbConnection connection = _conexion)
            {
                try
                {
                    connection.Open();

                    using (GridReader multi = await connection.QueryMultipleAsync(sql))
                    {
                        int exist = multi.ReadFirst<int>();
                        int activRepeated = multi.ReadFirst<int>();

                        if (exist <= 0)
                            Handlers.ExceptionClose(connection, "No se encontró la cotización solicitada");

                        if (activRepeated > 0)
                            Handlers.ExceptionClose(connection, "Ya existe una actividad con esa descripción");
                    }

                    string insert = $@"INSERT INTO ActividadProyecto
		                                        (descripcion, peso, idEstado, idPadre, idHermano, idCotizacion)
		                                        VALUES
		                                        (TRIM(@Descripcion), @Peso, 1, @IdPadre, @IdHermano, @IdCotizacion);";

                    int hasInsert = await connection.ExecuteAsync(insert, actividadModel);

                    if (hasInsert <= 0)
                        Handlers.ExceptionClose(connection, "Ocurrió un error al insertar la actividad");

                    return Handlers.CloseConnection(connection, trans, "Registro exitoso");
                }
                catch (SqlException err)
                {
                    throw err;
                }
            }
        }

        public async Task<ResponseModel> UpdateActividadCotizacion(ActividadCotizacionModel actividadModel)
        {
            string sql = $@"SELECT COUNT(1) 
							FROM ActividadProyecto 
							WHERE idActividad = {actividadModel.IdActividad};";
            sql += $@"SELECT COUNT(1)
					  FROM ActividadProyecto
					  WHERE idCotizacion = {actividadModel.IdCotizacion};";

            using (TransactionScope trans = new TransactionScope())
            using (IDbConnection connection = _conexion)
            {
                try
                {
                    connection.Open();

                    using (GridReader multi = await connection.QueryMultipleAsync(sql))
                    {
                        int existAct = multi.ReadFirst<int>();
                        int existCoti = multi.ReadFirst<int>();

                        if (existAct <= 0)
                            Handlers.ExceptionClose(connection, "No se encontró la actividad solicitada");

                        if (existCoti <= 0)
                            Handlers.ExceptionClose(connection, "No se encontró la cotización solicitada");
                    }

                    string sqlActRepeat = $@"SELECT COUNT(1) 
							                     FROM ActividadProyecto 
							                     WHERE descripcion LIKE TRIM(@Descripcion) 
								                    AND idPadre = @IdPadre
								                    AND idActividad <> @IdActividad
								                    AND idCotizacion = @IdCotizacion;";

                    int isRepeated = await connection.QueryFirstAsync<int>(sqlActRepeat, actividadModel);

                    if (isRepeated > 0)
                        Handlers.ExceptionClose(connection, "Ya existe una actividad con esa descripción");

                    string update = $@"UPDATE ActividadProyecto
		                                    SET descripcion = TRIM(@Descripcion),
			                                    peso = @Peso,
			                                    idPadre = @IdPadre,
			                                    idHermano = @IdHermano
		                                    WHERE idActividad = @IdActividad
                                                AND idCotizacion = @IdCotizacion;";

                    int hasUpdate = await connection.ExecuteAsync(update, actividadModel);

                    if (hasUpdate <= 0)
                        Handlers.ExceptionClose(connection, "Ocurrió un error al actualizar la actividad");

                    return Handlers.CloseConnection(connection, trans, "Actualización exitosa");
                }
                catch (SqlException err)
                {
                    throw err;
                }
            }
        }

        public async Task<ResponseModel> DeleteActividadCotizacion(int idCotizacion, int idActividad)
        {
            string sql = $@"SELECT COUNT(1) 
						    FROM ActividadProyecto 
						    WHERE idActividad = {idActividad};";
            sql += $@"SELECT COUNT(1) 
			          FROM ActividadProyecto 
				      WHERE idPadre = {idActividad};";

            sql += $@"SELECT COUNT(1) 
			          FROM ActividadProyecto 
				      WHERE idActividad = {idActividad};
                        AND idCotizacion = {idCotizacion}";

            using (TransactionScope trans = new TransactionScope())
            using (IDbConnection connection = _conexion)
            {
                try
                {
                    connection.Open();

                    using (GridReader multi = await connection.QueryMultipleAsync(sql))
                    {
                        int exist = multi.ReadFirst<int>();
                        int hasChild = multi.ReadFirst<int>();
                        int existCotizacion = multi.ReadFirst<int>();

                        if (exist <= 0)
                            Handlers.ExceptionClose(connection, "No se encontró la actividad solicitada");

                        if (hasChild > 0)
                            Handlers.ExceptionClose(connection, "Elimine los hijos de la actividad para continuar");

                        if (existCotizacion <= 0)
                            Handlers.ExceptionClose(connection, "No se encontró la cotizacion solicitada");
                    }

                    string delete = $@"DELETE FROM ActividadProyecto
		                                        WHERE idActividad = {idActividad};
                                                    AND idCotizacion = {idCotizacion}";

                    int hasDelete = await connection.ExecuteAsync(delete);

                    if (hasDelete <= 0)
                        Handlers.ExceptionClose(connection, "Ocurrió un error al eliminar la actividad");

                    return Handlers.CloseConnection(connection, trans, "Se eliminó el tipo de trabajador");
                }
                catch (SqlException err)
                {
                    throw err;
                }
            }
        }
    }
}