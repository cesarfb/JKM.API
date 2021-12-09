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

namespace JKM.PERSISTENCE.Repository.Usuario
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly IDbConnection _conexion;

        public UsuarioRepository(IDbConnection conexion)
        {
            _conexion = conexion;
        }

        public async Task<ResponseModel> RegisterUsuario(UsuarioModel usuarioModel)
        {
            using (TransactionScope trans = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            using (IDbConnection connection = _conexion)
            {
                try
                {
                    connection.Open();

                    //Comprobamos si ya existe un Usuario con nombre y apellido
                    string sql = $@"SELECT COUNT(1) 
                                    FROM 
                                        DetalleUsuario 
                                    WHERE 
                                        nombre = '{usuarioModel.Nombre}' and apellido = '{usuarioModel.Apellido}';";

                    int existeUsuario = await connection.QueryFirstAsync<int>(sql);

                    if (existeUsuario == 1)
                        Handlers.ExceptionClose(connection, "El nombre y/o apellido del Usuario ya se encuentra registrado");

                    //Comprobamos si ya existe un Usuario con nombre de usuario
                    string sqlNomUsuario = $@"  SELECT COUNT(1) 
                                                FROM 
                                                    Usuario 
                                                WHERE 
                                                    username = '{usuarioModel.Username}';";

                    int existeNomUsuario = await connection.QueryFirstAsync<int>(sqlNomUsuario);

                    if (existeNomUsuario == 1)
                        Handlers.ExceptionClose(connection, "El nombre de usuario ya se encuentra registrado");

                    //Registro Tabla Detalle Usuario
                    string insertDetalleUsuario = $@"INSERT INTO DetalleUsuario
                                                        (nombre, apellido, email, fechaNacimiento)
                                                    VALUES
                                                       (@Nombre, @Apellido, @Email, @FechaNacimiento);
                                                    
                                                    SELECT ISNULL(@@IDENTITY, -1)";


                    decimal hasInsertDetalleUsuario = (decimal)await connection.ExecuteScalarAsync(insertDetalleUsuario, usuarioModel);

                    if (hasInsertDetalleUsuario <= 0)
                        Handlers.ExceptionClose(connection, "Error al registrar el usuario");

                    //Registro Tabla Usuario
                    string insertUsuario = $@"INSERT INTO Usuario
                                                (username, password, idRol, idDetalleUsuario, idEstado)
                                            VALUES
                                                (@username, @password, @idRol, @idDetalleUsuario, 1)";

                    int hasInsertUsuario = await connection.ExecuteAsync(insertUsuario, new { username = usuarioModel.Username, password = usuarioModel.Password, idRol = usuarioModel.IdRol, idDetalleUsuario = hasInsertDetalleUsuario });

                    if (hasInsertUsuario <= 0)
                        Handlers.ExceptionClose(connection, "Error al registrar el Usuario");

                    return Handlers.CloseConnection(connection, trans, "Se registro el usuario");
                }
                catch (SqlException err)
                {
                    throw err;
                }

            }
        }

        public async Task<ResponseModel> UpdateUsuario(UsuarioModel usuarioModel)
        {
            //string sql = $@"SELECT COUNT(1) 
            //                FROM DetalleUsuario
            //                WHERE idDetalleUsuario = {usuarioModel.IdDetalleUsuario};";

            using (TransactionScope trans = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            using (IDbConnection connection = _conexion)
            {
                try
                {
                    connection.Open();

                    //using (GridReader multi = await connection.QueryMultipleAsync(sql))
                    //{
                    //    int clienteRepeated = multi.ReadFirst<int>();

                    //    if (clienteRepeated > 0)
                    //        Handlers.ExceptionClose(connection, "Ya existe un cliente con el RUC indicado");
                    //}

                    //Update Tabla DetalleUsuario
                    string updateDetalleUsuario = $@"UPDATE DetalleUsuario SET
		                                                nombre = @Nombre,
                                                        apellido = @Apellido,
                                                        email = @Email,
                                                        fechaNacimiento = @FechaNacimiento
                                                     WHERE 
                                                        idDetalleUsuario = @IdUsuario";

                    int hasUpdatedDetalleUsuario = await connection.ExecuteAsync(updateDetalleUsuario, usuarioModel);

                    if (hasUpdatedDetalleUsuario <= 0)
                        Handlers.ExceptionClose(connection, "Ocurrió un error al actualizar el Usuario");


                    //Update Tabla Usuario
                    string updateUsuario = $@"UPDATE Usuario SET
		                                          username = @Username,
                                                  password = @Password,
                                                  idRol = @IdRol
                                              WHERE 
                                                  idUsuario = @IdUsuario";

                    int hasUpdatedUsuario = await connection.ExecuteAsync(updateUsuario, usuarioModel);

                    if (hasUpdatedUsuario <= 0)
                        Handlers.ExceptionClose(connection, "Ocurrió un error al actualizar el Usuario");

                    return Handlers.CloseConnection(connection, trans, "Actualizacion exitosa");

                }
                catch (SqlException err)
                {
                    throw err;
                }
            }
        }

        public async Task<ResponseModel> UpdateEstado(int idUsuario)
        {

            using (TransactionScope trans = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            using (IDbConnection connection = _conexion)
            {
                try
                {
                    connection.Open();

                    //Comprobamos el estado del Usuario
                    string sql = $@"SELECT idEstado
                                    FROM Usuario
                                    WHERE idUsuario = {idUsuario};";

                    int idEstado = await connection.QueryFirstAsync<int>(sql);

                    //Si el Usuario se encuentra en estado Activo
                    if (idEstado == 1)
                    {
                        //Update Estado en Tabla Usuario
                        string updateEstadoUsuario = $@"UPDATE Usuario SET
		                                                idEstado = 2
                                                     WHERE 
                                                        idUsuario = {idUsuario}";

                        int hasUpdatedEstadoUsuario = await connection.ExecuteAsync(updateEstadoUsuario);

                        if (hasUpdatedEstadoUsuario <= 0)
                            Handlers.ExceptionClose(connection, "Ocurrió un error al actualizar el estado del Usuario");
                    }

                    //Si el Usuario se encuentra en estado Inactivo
                    else if (idEstado == 2)
                    {
                        //Update Estado en Tabla Usuario
                        string updateEstadoUsuario = $@"UPDATE Usuario SET
		                                                idEstado = 1
                                                     WHERE 
                                                        idUsuario = {idUsuario}";

                        int hasUpdatedEstadoUsuario = await connection.ExecuteAsync(updateEstadoUsuario);

                        if (hasUpdatedEstadoUsuario <= 0)
                            Handlers.ExceptionClose(connection, "Ocurrió un error al actualizar el estado del Usuario");
                    }

                    return Handlers.CloseConnection(connection, trans, "Actualizacion exitosa");
                }
                catch (SqlException err)
                {
                    throw err;
                }
            }

        }

        public async Task<ResponseModel> DeleteUsuario(int idDetalleUsuario)
        {
            string sql = $@"SELECT Count(1)
                            FROM DetalleUsuario
                            WHERE idDetalleUsuario = { idDetalleUsuario };";

            sql += $@"SELECT Count(1)
                      FROM Usuario
                      WHERE idUsuario = { idDetalleUsuario };";

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
                            Handlers.ExceptionClose(connection, "No se encontro el Usuario");

                        if (hasChild > 0)
                            Handlers.ExceptionClose(connection, "Elimine las cuentas del usuario para continuar");
                    }

                    string delete = $@"DELETE FROM DetalleUsuario
                                       WHERE idDetalleUsuario = { idDetalleUsuario }";

                    int hasDelete = await connection.ExecuteAsync(delete);

                    if (hasDelete <= 0)
                        Handlers.ExceptionClose(connection, "Ocurrio un erro al eliminar la venta");

                    return Handlers.CloseConnection(connection, trans, "Se elimino el usuario");
                }
                catch (SqlException err)
                {
                    throw err;
                }
            }
        }
    }
}
