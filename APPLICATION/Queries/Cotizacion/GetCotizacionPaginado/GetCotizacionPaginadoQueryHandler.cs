using Dapper;
using JKM.APPLICATION.Aggregates;
using JKM.UTILITY.Utils;
using MediatR;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace JKM.APPLICATION.Queries.Cotizacion.GetCotizacionPaginado
{
    public class GetCotizacionPaginadoQueryHandler : IRequestHandler<GetCotizacionPaginadoQuery, PaginadoResponse<CotizacionModel>>
    {
        private readonly IDbConnection _conexion;
        public GetCotizacionPaginadoQueryHandler(IDbConnection conexion)
        {
            _conexion = conexion;
        }

        public async Task<PaginadoResponse<CotizacionModel>> Handle(GetCotizacionPaginadoQuery request, CancellationToken cancellationToken)
        {
            string sql = $@"SELECT COUNT(1) FROM Cotizacion;";

            sql += $@"SELECT C.idCotizacion, C.solicitante, C.descripcion, C.fechaSolicitud,
                        C.descripcion, C.email, CLI.idCliente, CLI.razonSocial,
                        EC.idEstado, EC.descripcion as 'descripcionEstado',
                        C.precioCotizacion,
                        CASE WHEN C.idEstado = 1
	                        THEN 1 ELSE 0 END 'canEdit',
                        CASE WHEN C.idEstado = 1
	                        THEN 1 ELSE 0 END 'canDelete',
                        CASE WHEN C.idEstado = 1
			                        AND ISNULL(TRIM(C.descripcion),'') <> ''
			                        AND C.precioCotizacion > 0
			                        AND (SELECT COUNT(1) 
				                        FROM TipoTrabajadorCotizacion 
				                        WHERE idCotizacion = C.idCotizacion) > 0
			                        AND (SELECT COUNT(1) 
				                        FROM ActividadProyecto
				                        WHERE idCotizacion = C.idCotizacion) > 0
	                        THEN 1
	                        ELSE 0 END AS 'canCotizar'
                        FROM Cotizacion C 
                        INNER JOIN EstadoCotizacion EC  ON (C.idEstado = EC.idEstado)
                        INNER JOIN Cliente CLI ON CLI.idCliente=C.idCliente
	                    ORDER BY C.fechaSolicitud DESC
	                    OFFSET (({request.Pages} - 1) * {request.Rows}) ROWS FETCH NEXT {request.Rows} ROWS ONLY;";

            using (IDbConnection connection = _conexion)
            {
                try
                {
                    PaginadoResponse<CotizacionModel> newPaginado = new PaginadoResponse<CotizacionModel>();

                    connection.Open();
                    using (var multi = await connection.QueryMultipleAsync(sql))
                    {
                        newPaginado.TotalRows = multi.ReadFirst<int>();
                        newPaginado.Data = multi.Read<CotizacionModel>().AsList();
                        newPaginado.TotalPages = Math.Ceiling(newPaginado.TotalRows / request.Rows);
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