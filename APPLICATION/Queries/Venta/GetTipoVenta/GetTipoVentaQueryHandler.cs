using Dapper;
using JKM.UTILITY.Utils;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace JKM.APPLICATION.Queries.Venta.GetTipoVenta
{
    public class GetTipoVentaQueryHandler : IRequestHandler<GetTipoVentaQuery, IEnumerable<Identifier>>
    {
        private readonly IDbConnection _conexion;
        public GetTipoVentaQueryHandler(IDbConnection conexion)
        {
            _conexion = conexion;
        }

        public async Task<IEnumerable<Identifier>> Handle(GetTipoVentaQuery request, CancellationToken cancellationToken)
        {
            string sql = $@"SELECT idTipo 'id', descripcion 
	                        FROM TipoVenta";

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
