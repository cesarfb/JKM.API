using Dapper;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using static Dapper.SqlMapper;
using System.Transactions;
using JKM.UTILITY.Utils;

namespace JKM.PERSISTENCE.Repository.Proyecto
{
    public class ProyectoRepository : IProyectoRepository
    {
        private readonly IDbConnection _conexion;

        public ProyectoRepository(IDbConnection conexion)
        {
            _conexion = conexion;
        }

        public async Task<ResponseModel> RegisterProyecto(ProyectoModel proyectoModel)
        {
            using (TransactionScope trans = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            using (IDbConnection connection = _conexion)
            {
                try
                {
                    connection.Open();

                    string insert = $@"INSERT INTO Proyecto
										(nombreProyecto, fechaInicio, fechaFin, descripcion, idEstado)
										VALUES 
										(@NombreProyecto, @FechaInicio, @FechaFin, @Descripcion, 1);";


                    int hasInsert = await connection.ExecuteAsync(insert);

                    if (hasInsert <= 0)
                        Handlers.ExceptionClose(connection, "Error al registrar el proyecto");

                    return Handlers.CloseConnection(connection, trans, "Registro exitoso");
                }
                catch (SqlException err)
                {
                    throw err;
                }
            }
        }

        public async Task<ResponseModel> UpdateProyectoDetalle(ProyectoModel proyectoModel)
        {
            string sql = $@"SELECT COUNT(1) 
							FROM Proyecto 
							WHERE idProyecto = {proyectoModel.IdProyecto};";
            sql += $@"SELECT COUNT(1) 
					  FROM EstadoProyecto 
					  WHERE idEstado= {proyectoModel.IdEstado};";

            using (TransactionScope trans = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            using (IDbConnection connection = _conexion)
            {
                try
                {
                    connection.Open();

                    using (GridReader multi = await connection.QueryMultipleAsync(sql))
                    {
                        int existProy = multi.ReadFirst<int>();
                        int existEstado = multi.ReadFirst<int>();

                        if (existProy <= 0)
                            Handlers.ExceptionClose(connection, "No se encontró el proyecto");

                        if (existEstado <= 0)
                            Handlers.ExceptionClose(connection, "No se encontró el estado");
                    }

                    string update = $@"UPDATE Proyecto
		                                    SET nombre = @NombreProyecto,
			                                    descripcion = @Descripcion,
			                                    idEstado = @IdEstado
		                                    WHERE idProyecto = @IdProyecto;";

                    int hasUpdate = await connection.ExecuteAsync(update, proyectoModel);

                    if (hasUpdate <= 0)
                        Handlers.ExceptionClose(connection, "Error al actualizar el proyecto");

                    return Handlers.CloseConnection(connection, trans, "Se actualizó el proyecto");
                }
                catch (SqlException err)
                {
                    throw err;
                }
            }
        }

        public async Task<ResponseModel> RegisterTrabajadorProyecto(int idProyecto, int idTrabajador)
        {
            string sql = $@"SELECT COUNT(1) 
							FROM Proyecto 
							WHERE idProyecto = {idProyecto};";
            sql += $@"SELECT COUNT(1) 
					  FROM Trabajador 
					  WHERE idTrabajador = {idTrabajador};";

            sql += $@"SELECT COUNT(1) 
					  FROM ProyectoTrabajador 
					  WHERE idTrabajador = {idTrabajador};";

            using (TransactionScope trans = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            using (IDbConnection connection = _conexion)
            {
                try
                {
                    connection.Open();

                    using (GridReader multi = await connection.QueryMultipleAsync(sql))
                    {
                        int existProy = multi.ReadFirst<int>();
                        int existTrab = multi.ReadFirst<int>();
                        int existTrabProy = multi.ReadFirst<int>();

                        if (existProy <= 0)
                            Handlers.ExceptionClose(connection, "No se encontró el proyecto");

                        if (existTrab <= 0)
                            Handlers.ExceptionClose(connection, "No se encontró al trabajador");

                        if (existTrabProy > 0)
                            Handlers.ExceptionClose(connection, "El trabajador ya se encuentra asignado a un proyecto");
                    }

                    string insert = $@"INSERT INTO ProyectoTrabajador
		                                    (idTrabajador, idProyecto)
		                                    VALUES
		                                    (@IdTrabajador, @IdProyecto);";

                    int hasInsert = await connection.ExecuteAsync(insert, new { IdTrabajador = idTrabajador, IdProyecto = idProyecto });

                    if (hasInsert <= 0)
                        Handlers.ExceptionClose(connection, "No se pudo asignar al trabajador al proyecto");

                    return Handlers.CloseConnection(connection, trans, "Se asigno el trabajador al proyecto");
                }
                catch (SqlException err)
                {
                    throw err;
                }
            }
        }

        public async Task<ResponseModel> DeleteTrabajadorByProyecto(int idProyecto, int idTrabajador)
        {
            string sql = $@"SELECT COUNT(1) 
							FROM Proyecto 
							WHERE idProyecto = {idProyecto};";
            sql += $@"SELECT COUNT(1) 
							FROM Trabajador 
							WHERE idTrabajador = {idTrabajador};";

            using (TransactionScope trans = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            using (IDbConnection connection = _conexion)
            {
                try
                {
                    connection.Open();

                    using (GridReader multi = await connection.QueryMultipleAsync(sql))
                    {
                        int existProyecto = multi.ReadFirst<int>();
                        int existTrabajador = multi.ReadFirst<int>();

                        if (existProyecto <= 0)
                            Handlers.ExceptionClose(connection, "No se encontró el proyecto");
                        if (existTrabajador <= 0)
                            Handlers.ExceptionClose(connection, "No se encontró al trabajador");

                        string delete = $@"DELETE FROM ProyectoTrabajador
		                                    WHERE idProyecto = {idProyecto}
			                                    AND idTrabajador = {idTrabajador};";

                        int hasDelete = await connection.ExecuteAsync(delete);

                        if (hasDelete <= 0)
                            Handlers.ExceptionClose(connection, "Ocurrió un error al desvincular el trabajador");

                        string update = $@"UPDATE Trabajador
		                                    SET idEstado = 1
		                                    WHERE idTrabajador = {idTrabajador};";

                        int hasUpdate = await connection.ExecuteAsync(update);

                        if (hasUpdate <= 0)
                            Handlers.ExceptionClose(connection, "Ocurrió un error al desvincular el trabajador");

                        return Handlers.CloseConnection(connection, trans, "Se desvinculó al trabajador del proyecto");
                    }
                }
                catch (SqlException err)
                {
                    throw err;
                }
            }
        }

        public async Task<ResponseModel> UpdateActividadByProyecto(ActividadProyectoModel actividadModel)
        {
            string sql = $@"SELECT COUNT(1) 
							FROM ActividadProyecto 
							WHERE idActividad = {actividadModel.IdActividad};";


            using (TransactionScope trans = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            using (IDbConnection connection = _conexion)
            {
                try
                {
                    connection.Open();

                    using (GridReader multi = await connection.QueryMultipleAsync(sql))
                    {
                        int existAct = multi.ReadFirst<int>();

                        if (existAct <= 0)
                            Handlers.ExceptionClose(connection, "No se encontró la actividad");

                        string sqlAct = $@"SELECT COUNT(1) 
                                            FROM ActividadProyecto AP
                                            INNER JOIN Cotizacion C on AP.idCotizacion=C.idCotizacion
                                            INNER JOIN Venta V ON V.idCotizacion=C.idCotizacion
                                            INNER JOIN ProyectoVenta  PV ON PV.idVenta = V.idVenta
                                            WHERE AP.descripcion LIKE TRIM(@Descripcion) 
	                                            AND isnull(idPadre,0) = isnull(@IdPadre,0)
	                                            AND AP.idActividad <> @IdActividad
	                                            AND PV.idProyecto = @IdProyecto";

                        int repeatActiv = await connection.QueryFirstAsync<int>(sqlAct, actividadModel);

                        if (repeatActiv > 0)
                            Handlers.ExceptionClose(connection, "Ya existe una actividad con esa descripción");
                    }

                    string update = $@"UPDATE ActividadProyecto
		                                    SET descripcion = TRIM(@Descripcion),
			                                    peso = @Peso,
			                                    idPadre = @IdPadre,
			                                    idHermano = @IdHermano,
			                                    fechaInicio = @FechaInicio,
			                                    fechaFin = @FechaFin,
                                                idEstado = @IdEstado
		                                    WHERE idActividad = @IdActividad";

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
    }
}