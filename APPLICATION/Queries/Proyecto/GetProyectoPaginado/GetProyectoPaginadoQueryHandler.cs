using Dapper;
using JKM.APPLICATION.Aggregates;
using JKM.UTILITY.Utils;
using MediatR;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace JKM.APPLICATION.Queries.Proyecto.GetProyectoPaginado
{
    public class GetProyectoPaginadoQueryHandler : IRequestHandler<GetProyectoPaginadoQuery, PaginadoResponse<ProyectoModel>>
    {
        private readonly IDbConnection _conexion;
        public GetProyectoPaginadoQueryHandler(IDbConnection conexion)
        {
            _conexion = conexion;
        }

        public async Task<PaginadoResponse<ProyectoModel>> Handle(GetProyectoPaginadoQuery request, CancellationToken cancellationToken)
        {
            string sql = $@"SELECT COUNT(1) FROM Proyecto;";

            sql += $@"SELECT P.idProyecto, P.nombre as nombreProyecto, P.fechaInicio, 
						P.fechaFin, P.descripcion, EP.idEstado, EP.descripcion 'DescripcionEstado'
					  FROM Proyecto P
					  INNER JOIN EstadoProyecto EP ON (EP.idEstado = P.idEstado)
					  ORDER BY P.idProyecto DESC;";

            using (IDbConnection connection = _conexion)
            {
                try
                {
                    PaginadoResponse<ProyectoModel> newPaginado = new PaginadoResponse<ProyectoModel>();

                    connection.Open();
                    using (var multi = await connection.QueryMultipleAsync(sql))
                    {
                        newPaginado.TotalRows = multi.ReadFirst<int>();
                        newPaginado.Data = multi.Read<ProyectoModel>().AsList();
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