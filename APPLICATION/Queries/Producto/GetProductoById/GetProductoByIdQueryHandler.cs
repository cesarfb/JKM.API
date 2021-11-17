using Dapper;
using JKM.APPLICATION.Aggregates;
using MediatR;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace JKM.APPLICATION.Queries.Producto.GetProductoById
{
    public class GetProductoByIdQueryHandler : IRequestHandler<GetProductoByIdQuery, ProductoModel>
    {
        private readonly IDbConnection _conexion;
        public GetProductoByIdQueryHandler(IDbConnection conexion)
        {
            _conexion = conexion;
        }

        public async Task<ProductoModel> Handle(GetProductoByIdQuery request, CancellationToken cancellationToken)
        {
            string sql = $@"SELECT idProducto, nombre, codigo, imagen 
                            FROM Producto
                            WHERE idProducto = @IdProducto";

            using (IDbConnection connection = _conexion)
            {
                try
                {
                    connection.Open();
                    ProductoModel productos =
                        await connection.QueryFirstOrDefaultAsync<ProductoModel>(sql, request);
                    connection.Close();

                    if (productos == null) throw new ArgumentNullException();

                    return productos;
                }
                catch (SqlException err)
                {
                    throw err;
                }
            }
        }
    }
}
