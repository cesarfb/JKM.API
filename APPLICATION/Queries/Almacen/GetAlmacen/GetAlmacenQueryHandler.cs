using Dapper;
using JKM.APPLICATION.Aggregates;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace JKM.APPLICATION.Queries.Almacen.GetAlmacen
{
    public class GetAlmacenQueryHandler : IRequestHandler<GetAlmacenQuery, IEnumerable<AlmacenModel>>
    {
        private readonly IDbConnection _conexion;
        public GetAlmacenQueryHandler(IDbConnection conexion)
        {
            _conexion = conexion;
        }
        public async Task<IEnumerable<AlmacenModel>> Handle(GetAlmacenQuery request, CancellationToken cancellationToken)
        {
            string sql = $@"SELECT idAlmacen, nombre, direccion, distrito
                            FROM Almacen";

            using (IDbConnection connection = _conexion)
            {
                try
                {
                    connection.Open();

                    IEnumerable<AlmacenModel> almacen = await connection.QueryAsync<AlmacenModel>(sql, request);

                    connection.Close();

                    if (almacen == null) throw new ArgumentNullException();

                    return almacen;
                }
                catch (SqlException err)
                {
                    throw err;
                }
            }
        }
    }
}
