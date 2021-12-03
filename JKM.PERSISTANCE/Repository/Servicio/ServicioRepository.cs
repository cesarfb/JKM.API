using Dapper;
using JKM.UTILITY.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace JKM.PERSISTENCE.Repository.Servicio
{
    public class ServicioRepository : IServicioRepository
    {
        private readonly IDbConnection _conexion;

        public ServicioRepository(IDbConnection conexion)
        {
            _conexion = conexion;
        }

        public async Task<ResponseModel> RegisterServicio(ServicioModel servicioModel)
        {
            using (TransactionScope trans = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            using (IDbConnection connection = _conexion)
            {
                try
                {
                    connection.Open();

                    //Registro Tabla Servicio
                    string insertServicio = $@"INSERT INTO Servicio
                                                   (nombre, imagen, descripcion, isActive)
                                               VALUES
                                                   (@nombre, @imagen, @descripcion, 1)";

                    int hasInsertServicio = await connection.ExecuteAsync(insertServicio, servicioModel);

                    if (hasInsertServicio <= 0)
                        Handlers.ExceptionClose(connection, "Error al registrar el Servicio");

                    return Handlers.CloseConnection(connection, trans, "Se registro el Servicio");
                }

                catch (SqlException err)
                {
                    throw err;
                }
            }
        }

        public async Task<ResponseModel> UpdateServicio(ServicioModel servicioModel)
        {

            using (TransactionScope trans = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            using (IDbConnection connection = _conexion)
            {
                try
                {
                    connection.Open();

                    //Update Tabla Servicio
                    string updateServicio = $@"UPDATE Servicio SET
		                                          nombre = @Nombre,
                                                  imagen = @Imagen,
                                                  descripcion = @Descripcion
                                              WHERE 
                                                  idServicio = @IdServicio";

                    int hasUpdatedServicio = await connection.ExecuteAsync(updateServicio, servicioModel);

                    if (hasUpdatedServicio <= 0)
                        Handlers.ExceptionClose(connection, "Ocurrió un error al actualizar el Servicio");

                    return Handlers.CloseConnection(connection, trans, "Actualizacion exitosa");

                }

                catch (SqlException err)
                {
                    throw err;
                }
            }
        }

        public async Task<ResponseModel> UpdateEstadoServicio(int idServicio)
        {

            using (TransactionScope trans = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            using (IDbConnection connection = _conexion)
            {
                try
                {
                    connection.Open();

                    //Comprobamos el estado del Servicio
                    string sql = $@"SELECT 
                                        isActive
                                    FROM 
                                        Servicio
                                    WHERE 
                                        idServicio = {idServicio};";

                    int estado = await connection.QueryFirstAsync<int>(sql);

                    //Si el Servicio se encuentra en estado Activo
                    if (estado == 1)
                    {
                        //Update Estado en Tabla Servicio
                        string updateEstadoServicio = $@"UPDATE Servicio SET
		                                                    isActive = 0
                                                         WHERE 
                                                            idServicio = {idServicio}";

                        int hasUpdatedEstadoServicio = await connection.ExecuteAsync(updateEstadoServicio);

                        if (hasUpdatedEstadoServicio <= 0)
                            Handlers.ExceptionClose(connection, "Ocurrió un error al actualizar el estado del Servicio");
                    }

                    //Si el Servicio se encuentra en estado Inactivo
                    else if (estado == 0)
                    {
                        //Update Estado en Tabla Servicio
                        string updateEstadoServicio = $@"UPDATE Servicio SET
		                                                    isActive = 1
                                                         WHERE 
                                                            idServicio = {idServicio}";

                        int hasUpdatedEstadoServicio = await connection.ExecuteAsync(updateEstadoServicio);

                        if (hasUpdatedEstadoServicio <= 0)
                            Handlers.ExceptionClose(connection, "Ocurrió un error al actualizar el estado del Servicio");
                    }

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
