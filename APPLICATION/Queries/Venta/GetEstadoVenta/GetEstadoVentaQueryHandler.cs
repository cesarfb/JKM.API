using Dapper;
using JKM.UTILITY.Utils;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace JKM.APPLICATION.Queries.Venta.GetEstadoVenta
{
    public class GetEstadoVentaQueryHandler : IRequestHandler<GetEstadoVentaQuery, IEnumerable<Identifier>>
    {
        private readonly IDbConnection _conexion;
        public GetEstadoVentaQueryHandler(IDbConnection conexion)
        {
            _conexion = conexion;
        }

        public async Task<IEnumerable<Identifier>> Handle(GetEstadoVentaQuery request, CancellationToken cancellationToken)
        {
            string sql = $@"SELECT idEstado 'id', descripcion 
	                        FROM EstadoVenta";

            using (IDbConnection connection = _conexion)
            {
                try
                {
                    connection.Open();
                    IEnumerable<Identifier> response =
                        await connection.QueryAsync<Identifier>(sql);
                    connection.Close();

                    if (response.AsList().Count == 0) throw new ArgumentNullException();

                    return response;
                }
                catch (SqlException err)
                {
                    throw err;
                }
            }
        }
    }
}
