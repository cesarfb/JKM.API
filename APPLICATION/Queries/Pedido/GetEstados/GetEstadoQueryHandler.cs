using Dapper;
using JKM.UTILITY.Utils;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace JKM.APPLICATION.Queries.Pedido.GetEstados
{
    public class GetEstadoQueryHandler : IRequestHandler<GetEstadoQuery, IEnumerable<Identifier>>
    {
        private readonly IDbConnection _conexion;
        public GetEstadoQueryHandler(IDbConnection conexion)
        {
            _conexion = conexion;
        }

        public async Task<IEnumerable<Identifier>> Handle(GetEstadoQuery request, CancellationToken cancellationToken)
        {
            string sql = $@"SELECT idEstado as id, descripcion
					          FROM EstadoPedido";

            using (IDbConnection connection = _conexion)
            {
                try
                {
                    connection.Open();
                    IEnumerable<Identifier> estados =
                            await connection.QueryAsync<Identifier>(sql);
                    connection.Close();

                    if (estados.AsList().Count == 0) throw new ArgumentNullException();
                    return estados;
                }
                catch (SqlException err)
                {
                    throw err;
                }
            }
        }
    }
}
