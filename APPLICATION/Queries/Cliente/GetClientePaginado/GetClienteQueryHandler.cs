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

namespace JKM.APPLICATION.Queries.Cliente.GetClientePaginado
{
    public class GetClienteQueryHandler : IRequestHandler<GetClienteQuery, IEnumerable<ClienteModel>>
    {
        private readonly IDbConnection _conexion;
        public GetClienteQueryHandler(IDbConnection conexion)
        {
            _conexion = conexion;
        }

        public async Task<IEnumerable<ClienteModel>> Handle(GetClienteQuery request, CancellationToken cancellationToken)
        {
            string sql = $@"SELECT C.idCliente, C.ruc, C.razonSocial, C.telefono
	                    FROM Cliente C
                        ORDER BY C.razonSocial DESC;";

            using (IDbConnection connection = _conexion)
            {
                try
                {
                    connection.Open();
                    IEnumerable<ClienteModel> clientes =
                        await connection.QueryAsync<ClienteModel>(sql);

                    connection.Close();

                    if (clientes.AsList().Count == 0) throw new ArgumentNullException();

                    return clientes;
                }
                catch (SqlException err)
                {
                    throw err;
                }
            }
        }
    }
}
