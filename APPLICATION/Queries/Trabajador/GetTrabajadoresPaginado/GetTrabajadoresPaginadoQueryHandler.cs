using Dapper;
using JKM.APPLICATION.Aggregates;
using JKM.UTILITY.Utils;
using MediatR;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace JKM.APPLICATION.Queries.Trabajador.GetTrabajadoresPaginado
{
    public class GetTrabajadoresPaginadoQueryHandler : IRequestHandler<GetTrabajadoresPaginadoQuery, PaginadoResponse<TrabajadorModel>>
    {
        private readonly IDbConnection _conexion;
        public GetTrabajadoresPaginadoQueryHandler(IDbConnection conexion)
        {
            _conexion = conexion;
        }

        public async Task<PaginadoResponse<TrabajadorModel>> Handle(GetTrabajadoresPaginadoQuery request, CancellationToken cancellationToken)
        {
           string sql = $@"SELECT COUNT(1) 
                            FROM Trabajador T
                            WHERE @idEstado = (CASE WHEN @idEstado <> 0 THEN
							    T.idEstado ELSE 0 END)
							    AND @idTipo  = (CASE WHEN @idTipo <> 0 THEN
								T.idTipoTrabajador ELSE 0 END);";

            sql += $@"SELECT T.idTrabajador, T.nombre, T.apellidoPaterno, T.apellidoMaterno, T.fechaNacimiento,
								ET.idEstado, ET.descripcion 'descripcionEstado',
								TT.idTipoTrabajador, TT.descripcion 'descripcionTipo', TT.precioReferencial
					  FROM Trabajador T 
					  INNER JOIN EstadoTrabajador ET ON ET.idEstado = T.idEstado
					  INNER JOIN TipoTrabajador TT ON TT.idTipoTrabajador = T.idTipoTrabajador
					  WHERE @idEstado = (CASE WHEN @idEstado <> 0 
                                                THEN T.idEstado ELSE 0 END)
							AND @idTipo  = (CASE WHEN @idTipo <> 0 THEN
												   T.idTipoTrabajador ELSE 0 END)
					  ORDER BY T.idTrabajador DESC;";

			using (IDbConnection connection = _conexion)
			{
                try
                {
                    PaginadoResponse<TrabajadorModel> newPaginado = new PaginadoResponse<TrabajadorModel>();

                    connection.Open();

                    using (var multi = await connection.QueryMultipleAsync(sql, request))
                    {
                        newPaginado.TotalRows = multi.ReadFirst<int>();
                        newPaginado.Data = multi.Read<TrabajadorModel>().AsList();
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
