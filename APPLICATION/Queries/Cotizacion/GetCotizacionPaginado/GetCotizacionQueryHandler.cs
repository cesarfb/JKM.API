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

namespace JKM.APPLICATION.Queries.Cotizacion.GetCotizacionPaginado
{
    public class GetCotizacionQueryHandler : IRequestHandler<GetCotizacion, IEnumerable<CotizacionModel>>
    {
        private readonly IDbConnection _conexion;
        public GetCotizacionQueryHandler(IDbConnection conexion)
        {
            _conexion = conexion;
        }

        public async Task<IEnumerable<CotizacionModel>> Handle(GetCotizacion request, CancellationToken cancellationToken)
        {
            string sql = $@"SELECT C.idCotizacion, C.solicitante, C.descripcion, C.fechaSolicitud,
                        C.descripcion, C.email, CLI.idCliente, CLI.razonSocial,
                        EC.idEstado, EC.descripcion as 'descripcionEstado',TIPO.descripcion as 'descripcionTipoCotizacion',
                        C.precioCotizacion,
                        CASE WHEN C.idEstado = 1
	                        THEN 1 ELSE 0 END 'canEdit',
                        CASE WHEN C.idEstado = 1
	                        THEN 1 ELSE 0 END 'canDelete',
                        CASE WHEN C.idEstado = 1
                                    AND ((C.idTipoCotizacion = 1
			                        AND ISNULL(TRIM(C.descripcion),'') <> ''
			                        AND isnull(C.precioCotizacion,0) > 0
			                        AND (SELECT COUNT(1) 
				                        FROM TipoTrabajadorCotizacion 
				                        WHERE idCotizacion = C.idCotizacion) > 0
			                        AND (SELECT COUNT(1) 
				                        FROM ActividadProyecto
				                        WHERE idCotizacion = C.idCotizacion) > 0)
                                    OR (C.idTipoCotizacion = 2
                                        AND (SELECT COUNT(1) 
				                        FROM DetalleOrden
				                        WHERE idCotizacion = C.idCotizacion) > 0)
                                        AND isnull(C.precioCotizacion,0)  > 0)
	                        THEN 1
	                        ELSE 0 END AS 'canCotizar'
                        FROM Cotizacion C 
                        INNER JOIN EstadoCotizacion EC  ON (C.idEstado = EC.idEstado)
                        INNER JOIN Cliente CLI ON CLI.idCliente=C.idCliente
                        INNER JOIN TipoCotizacion TIPO  ON TIPO.idTipoCotizacion = C.idTipoCotizacion 
	                    ORDER BY canEdit desc, C.fechaSolicitud DESC;";

            using (IDbConnection connection = _conexion)
            {
                try
                {
                    connection.Open();
                    IEnumerable<CotizacionModel> cotizaciones =
                        await connection.QueryAsync<CotizacionModel>(sql);

                    connection.Close();

                    if (cotizaciones.AsList().Count == 0) throw new ArgumentNullException();

                    return cotizaciones;
                }
                catch (SqlException err)
                {
                    throw err;
                }
            }
        }
    }
}