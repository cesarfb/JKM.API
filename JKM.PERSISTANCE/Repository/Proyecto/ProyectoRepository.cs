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
		                                    SET nombreProyecto = @Nombre,
			                                    descripcion = @Descripcion,
			                                    fechaInicio = @FechaInicio,
			                                    fechaFin = @FechaFin,
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
					  FROM TrabajadorProyecto 
					  WHERE idProyecto = {idProyecto} 
					  	AND idTrabajador = {idTrabajador};";

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

                        if (existTrabProy >= 0)
                            Handlers.ExceptionClose(connection, "El trabajador ya se encuentra asignado a un proyecto");
                    }

                    string insert = $@"INSERT INTO TrabajadorProyecto
		                                    (idTrabajador, idProyecto)
		                                    VALUES
		                                    (@IdTrabajador, @IdProyecto);";

                    int hasInsert = await connection.ExecuteAsync(insert, new { IdTrabajador = idTrabajador, IdProyecto = idProyecto });

                    if (hasInsert <= 0)
                        Handlers.ExceptionClose(connection, "No se pudo asignar al trabajador al proyecto");

                    string update = $@"UPDATE Trabajador
		                                    SET idEstado = 3
		                                    WHERE idTrabajador = {idTrabajador};";

                    int hasUpdate = await connection.ExecuteAsync(update);

                    if (hasUpdate <= 0)
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

                        string delete = $@"DELETE FROM TrabajadorProyecto
		                                    WHERE idProyecto = {idProyecto}
			                                    AND idTrabajador = {idTrabajador};";

                        int hasDelete = await connection.ExecuteAsync(delete);

                        if (existTrabajador <= 0)
                            Handlers.ExceptionClose(connection, "Ocurrió un error al desvincular el trabajador");

                        string update = $@"UPDATE Trabajador
		                                    SET idEstado = 2
		                                    WHERE idTrabajador = {idTrabajador};";

                        int hasUpdate = await connection.ExecuteAsync(delete);

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
            sql += $@"SELECT idProyecto
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
                        int idProyecto = multi.ReadFirst<int>();

                        if (existAct <= 0)
                            Handlers.ExceptionClose(connection, "No se encontró la actividad");
                        if (idProyecto <= 0)
                            Handlers.ExceptionClose(connection, "No se encontró el proyecto");

                        actividadModel.IdProyecto = idProyecto;

                        string sqlAct = $@"SELECT COUNT(1) 
							                FROM ActividadProyecto 
							                WHERE descripcion LIKE TRIM(@Descripcion) 
								                AND idPadre = @IdPadre
								                AND idActividad <> @IdActividad
								                AND idProyecto = @IdProyecto)";

                        int repeatActiv = await connection.ExecuteAsync(sql, actividadModel);

                        if (repeatActiv >= 0)
                            Handlers.ExceptionClose(connection, "Ya existe una actividad con esa descripción");
                    }

                    string update = $@"UPDATE ActividadProyecto
		                                    SET descripcion = TRIM(@Descripcion),
			                                    peso = @Peso,
			                                    idPadre = @IdPadre,
			                                    idHermano = @IdHermano,
			                                    fechaInicio = @FechaInicio,
			                                    fechaFin = @FechaFin
		                                    WHERE idActividad = @IdActividad";

                    int hasUpdate = await connection.ExecuteAsync(update, actividadModel);

                    if (hasUpdate <= 0)
                        Handlers.ExceptionClose(connection, "Ocurrió un error al actualizar la actividad");

                    string updateDate = $@"DECLARE @StartDate DATE = (SELECT MIN(fechaInicio) 
											                                FROM ActividadProyecto
											                                WHERE idProyecto = {actividadModel.IdProyecto}),
						                                @EndDate DATE = (SELECT MAX(fechaFin) 
											                                FROM ActividadProyecto
											                                WHERE idProyecto = {actividadModel.IdProyecto});

				                                UPDATE Proyecto
				                                SET fechaInicio = @StartDate,
					                                fechaFin = @EndDate
				                                WHERE idProyecto = @IdProyecto;";

                    int hasUpdateDate = await connection.ExecuteAsync(updateDate);

                    if (hasUpdateDate <= 0)
                        Handlers.ExceptionClose(connection, "Ocurrio un error al actualizar la fecha de la actividad");

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