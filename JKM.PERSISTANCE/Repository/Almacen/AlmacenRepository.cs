using Dapper;
using JKM.UTILITY.Utils;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Transactions;
using static Dapper.SqlMapper;

namespace JKM.PERSISTENCE.Repository.Almacen
{
    public class AlmacenRepository : IAlmacenRepository
    {
        private readonly IDbConnection _conexion;
        public AlmacenRepository(IDbConnection conexion)
        {
            _conexion = conexion;
        }
        public async Task<ResponseModel> RegisterAlmacen(AlmacenModel model)
        {
            string sql = $@"SELECT COUNT(1)
                            FROM Almacen
                            WHERE nombre = @Nombre;";
            sql += $@"SELECT COUNT(1)
                        FROM Almacen
                        WHERE direccion = @Direccion;";
            using (TransactionScope trans = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            using (IDbConnection connection = _conexion)
            {
                try
                {
                    connection.Open();

                    using (GridReader multi = await connection.QueryMultipleAsync(sql, model))
                    {
                        int existNombre = multi.ReadFirst<int>();
                        int existDireccion = multi.ReadFirst<int>();

                        if (existNombre > 0)
                            Handlers.ExceptionClose(connection, "El nombre ya se encuentra en uso");

                        if (existDireccion > 0)
                            Handlers.ExceptionClose(connection, "La direccion del almacen ya se encuentra en uso");

                        string insertAlamcen = $@"INSERT INTO Almacen
                                                (nombre, direccion, distrito)
                                                VALUES 
                                                (@Nombre, @Direccion, @Distrito);
                                                SELECT SCOPE_IDENTITY()";

                        int idAlmacen = await connection.QueryFirstOrDefaultAsync<int>(insertAlamcen, model);

                        if (idAlmacen <= 0)
                            Handlers.ExceptionClose(connection, "Ocurrió un error al registrar el almacen");

                        return Handlers.CloseConnection(connection, trans, "Registro exitoso");
                    }
                }
                catch (SqlException err)
                {
                    throw err;
                }
            }
        }

        public async Task<ResponseModel> UpdateAlmacen(AlmacenModel model)
        {
            string sql = $@"SELECT COUNT(1)
                            FROM Almacen
                            WHERE nombre = @Nombre
                            AND idAlmacen <> @IdAlmacen;";
            sql += $@"SELECT COUNT(1)
                        FROM Almacen
                        WHERE direccion = @Direccion
                        AND idAlmacen <> @IdAlmacen;";
            sql += $@"SELECT COUNT(1)
                        FROM Almacen
                        WHERE idAlmacen = @IdAlmacen;";

            using (TransactionScope trans = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            using (IDbConnection connection = _conexion)
            {
                try
                {
                    connection.Open();

                    using (GridReader multi = await connection.QueryMultipleAsync(sql, model))
                    {
                        int existNombre = multi.ReadFirst<int>();
                        int existDireccion = multi.ReadFirst<int>();
                        int existAlmacen = multi.ReadFirst<int>();

                        if (existNombre > 0)
                            Handlers.ExceptionClose(connection, "El nombre ya se encuentra en uso");

                        if (existDireccion > 0)
                            Handlers.ExceptionClose(connection, "La direccion del almacen ya se encuentra en uso");

                        if (existAlmacen != 1)
                            Handlers.ExceptionClose(connection, "No se encontro el almacen");

                        string updateAlmacen = $@"UPDATE Almacen
                                                    SET nombre = @Nombre,
                                                        direccion = @Direccion,
                                                        distrito = @Distrito
                                                    WHERE idAlmacen = @IdAlmacen;";

                        int rows = await connection.ExecuteAsync(updateAlmacen, model);

                        if (rows <= 0)
                            Handlers.ExceptionClose(connection, "Ocurrió un error al actualizar el almacen");

                        return Handlers.CloseConnection(connection, trans, "Actualizacion exitosa");
                    }
                }
                catch (SqlException err)
                {
                    throw err;
                }
            }
        }
    }
}
