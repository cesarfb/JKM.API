using Dapper;
using JKM.UTILITY.Utils;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace JKM.APPLICATION.Queries.Cotizacion.GetEstadoCotizacion
{
    public class GetEstadoCotizacionQueryHandler : IRequestHandler<GetEstadoCotizacionQuery, IEnumerable<Identifier>>
    {
        private readonly IDbConnection _conexion;
        public GetEstadoCotizacionQueryHandler(IDbConnection conexion)
        {
            _conexion = conexion;
        }

        public async Task<IEnumerable<Identifier>> Handle(GetEstadoCotizacionQuery request, CancellationToken cancellationToken)
        {
            string sql = $@"SELECT idEstado as id, descripcion
			                FROM EstadoCotizacion";

            using (IDbConnection connection = _conexion)
            {
                try
                {
                    connection.Open();
                    IEnumerable<Identifier> estado =
                        await connection.QueryAsync<Identifier>(sql);
                    connection.Close();

                    if (estado.AsList().Count == 0) throw new ArgumentNullException();

                    return estado;
                }
                catch (SqlException err)
                {
                    throw err;
                }
            }
        }
    }
}
