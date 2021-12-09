using Dapper;
using JKM.UTILITY.Utils;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Transactions;
using static Dapper.SqlMapper;

namespace JKM.PERSISTENCE.Repository.Producto
{
    public class ProductoRepository : IProductoRepository
    {
        private readonly IDbConnection _conexion;

        public ProductoRepository(IDbConnection conexion)
        {
            _conexion = conexion;
        }

        public async Task<ResponseModel> RegisterProducto(ProductoModel model)
        {
            string sql = $@"SELECT COUNT(1)
                            FROM Producto
                            WHERE nombre = @Nombre;";

            sql += $@"SELECT COUNT(1)
                     FROM Producto
                     WHERE codigo = @Codigo;";

            using (TransactionScope trans = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            using (IDbConnection connection = _conexion)
            {
                try
                {
                    connection.Open();

                    using (GridReader multi = await connection.QueryMultipleAsync(sql, model))
                    {
                        int existNombre = multi.ReadFirst<int>();
                        int existCodigo = multi.ReadFirst<int>();

                        if (existNombre > 0)
                            Handlers.ExceptionClose(connection, "El nombre ingresa ya se encuentra en uso");

                        if (existCodigo > 0)
                            Handlers.ExceptionClose(connection, "El codigo ingresa ya se encuentra en uso");
                    }

                    string insert = $@"INSERT INTO Producto
                                       (nombre, codigo, imagen)
                                       VALUES
                                       (@Nombre, @Codigo, @Imagen)";

                    int hasInsert = await connection.ExecuteAsync(insert, model);

                    if (hasInsert <= 0)
                        Handlers.ExceptionClose(connection, "Error al registrar el producto");

                    return Handlers.CloseConnection(connection, trans, "Se registró el producto");
                }
                catch (SqlException err)
                {
                    throw err;
                }
            }
        }

        public async Task<ResponseModel> UpdateProducto(ProductoModel model)
        {
            string sql = $@"SELECT COUNT(1)
                            FROM Producto
                            WHERE nombre = @Nombre
                            AND idProducto <> @IdProducto;";

            sql += $@"SELECT COUNT(1)
                     FROM Producto
                     WHERE codigo = @Codigo
                     AND idProducto <> @IdProducto;";

            using (TransactionScope trans = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            using (IDbConnection connection = _conexion)
            {
                try
                {
                    connection.Open();

                    using (GridReader multi = await connection.QueryMultipleAsync(sql, model))
                    {
                        int existNombre = multi.ReadFirst<int>();
                        int existCodigo = multi.ReadFirst<int>();

                        if (existNombre > 0)
                            Handlers.ExceptionClose(connection, "El nombre ingresa ya se encuentra en uso");

                        if (existCodigo > 0)
                            Handlers.ExceptionClose(connection, "El codigo ingresa ya se encuentra en uso");
                    }

                    string update = $@"UPDATE Producto
                                        SET nombre = @Nombre,
                                            codigo = @Codigo,
                                            imagen = @Imagen
                                        WHERE idProducto = @IdProducto";

                    int hasUpdate = await connection.ExecuteAsync(update, model);

                    if (hasUpdate <= 0)
                        Handlers.ExceptionClose(connection, "Error al actualizar el producto");

                    return Handlers.CloseConnection(connection, trans, "Se actualizó el producto");
                }
                catch (SqlException err)
                {
                    throw err;
                }
            }
        }
    }
}
