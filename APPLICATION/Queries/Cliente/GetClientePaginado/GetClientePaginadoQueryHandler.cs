using Dapper;
using JKM.APPLICATION.Aggregates;
using JKM.UTILITY.Utils;
using MediatR;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace JKM.APPLICATION.Queries.Cliente.GetClientePaginado
{
    public class GetClientePaginadoQueryHandler : IRequestHandler<GetClientePaginadoQuery, PaginadoResponse<ClienteModel>>
    {
        private readonly IDbConnection _conexion;
        public GetClientePaginadoQueryHandler(IDbConnection conexion)
        {
            _conexion = conexion;
        }

        public async Task<PaginadoResponse<ClienteModel>> Handle(GetClientePaginadoQuery request, CancellationToken cancellationToken)
        {
            string sql = $@"SELECT COUNT(1) FROM Cliente;";

            sql += $@"SELECT C.idCliente, C.ruc, C.razonSocial, C.telefono
	                    FROM Cliente C
                        ORDER BY C.razonSocial DESC;";

            using (IDbConnection connection = _conexion)
            {
                try
                {
                    PaginadoResponse<ClienteModel> newPaginado = new PaginadoResponse<ClienteModel>();

                    connection.Open();
                    using (var multi = await connection.QueryMultipleAsync(sql))
                    {
                        newPaginado.TotalRows = multi.ReadFirst<int>();
                        newPaginado.Data = multi.Read<ClienteModel>().AsList();
                    }
                    connection.Close();

                    if (newPaginado.Data.AsList().Count == 0) throw new ArgumentNullException();

                    return newPaginado;
                }
                catch (SqlException err)
                {
                    throw err;
                }
            }
        }
    }
}
