using Dapper;
using JKM.APPLICATION.Aggregates;
using JKM.UTILITY.Utils;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace JKM.APPLICATION.Queries.Producto.GetProducto
{
    public class GetProductoQueryHandler : IRequestHandler<GetProductoQuery, IEnumerable<ProductoModel>>
    {
        private readonly IDbConnection _conexion;
        public GetProductoQueryHandler(IDbConnection conexion)
        {
            _conexion = conexion;
        }

        public async Task<IEnumerable<ProductoModel>> Handle(GetProductoQuery request, CancellationToken cancellationToken)
        {
            string sql = $@"SELECT idProducto, nombre, codigo, imagen FROM Producto";

            using (IDbConnection connection = _conexion)
            {
                try
                {
                    connection.Open();
                    IEnumerable<ProductoModel> productos =
                        await connection.QueryAsync<ProductoModel>(sql);
                    connection.Close();

                    if (productos.AsList().Count == 0) throw new ArgumentNullException();

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
