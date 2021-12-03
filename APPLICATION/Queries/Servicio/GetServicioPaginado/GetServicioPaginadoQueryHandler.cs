using Dapper;
using JKM.APPLICATION.Aggregates;
using JKM.UTILITY.Utils;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JKM.APPLICATION.Queries.Servicio.GetServicioPaginado
{
    public class GetServicioPaginadoQueryHandler : IRequestHandler<GetServicioPaginadoQuery, PaginadoResponse<ServicioModel>>
    {
        private readonly IDbConnection _conexion;

        public GetServicioPaginadoQueryHandler(IDbConnection conexion)
        {
            _conexion = conexion;
        }

        public async Task<PaginadoResponse<ServicioModel>> Handle(GetServicioPaginadoQuery request, CancellationToken cancellationToken)
        {

            string sql = $@"SELECT COUNT(1) FROM Servicio;";

            sql += $@"select
		                idServicio,
		                nombre,
		                imagen,
		                descripcion,
		                isActive
	                  from 
		                Servicio";

            using (IDbConnection connection = _conexion)
            {
                try
                {
                    PaginadoResponse<ServicioModel> newPaginado = new PaginadoResponse<ServicioModel>();

                    connection.Open();

                    using (var multi = await connection.QueryMultipleAsync(sql, request))
                    {
                        newPaginado.TotalRows = multi.ReadFirst<int>();
                        newPaginado.Data = multi.Read<ServicioModel>().AsList();
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
