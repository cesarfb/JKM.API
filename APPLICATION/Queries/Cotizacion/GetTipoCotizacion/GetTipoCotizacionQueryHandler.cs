using Dapper;
using JKM.UTILITY.Utils;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;


namespace JKM.APPLICATION.Queries.Cotizacion.GetTipoCotizacion
{
    public class GetTipoCotizacionQueryHandler : IRequestHandler<GetTipoCotizacionQuery, IEnumerable<Identifier>>
    {
        private readonly IDbConnection _conexion;
        public GetTipoCotizacionQueryHandler(IDbConnection conexion)
        {
            _conexion = conexion;
        }

        public async Task<IEnumerable<Identifier>> Handle(GetTipoCotizacionQuery request, CancellationToken cancellationToken)
        {
            string sql = $@"SELECT idTipoCotizacion as id, descripcion
			                FROM TipoCotizacion";

            using (IDbConnection connection = _conexion)
            {
                try
                {
                    connection.Open();
                    IEnumerable<Identifier> tipo =
                        await connection.QueryAsync<Identifier>(sql);
                    connection.Close();

                    if (tipo.AsList().Count == 0) throw new ArgumentNullException();

                    return tipo;
                }
                catch (SqlException err)
                {
                    throw err;
                }
            }
        }
    }
}
