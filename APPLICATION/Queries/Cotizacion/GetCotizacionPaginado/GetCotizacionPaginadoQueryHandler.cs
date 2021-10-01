using Dapper;
using FluentValidation;
using FluentValidation.Results;
using JKM.APPLICATION.Aggregates;
using JKM.PERSISTENCE.Utils;
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
            ValidationResult validator = new Validator().Validate(request);
            Handlers.HandlerException(validator);

            string sql = $@"SELECT COUNT(1) FROM Cotizacion;";

            sql += $@"SELECT C.idCotizacion, C.solicitante, C.descripcion, C.fechaSolicitud,
			            C.descripcion, C.email, C.empresa,
			            EC.idEstado, EC.descripcion as 'descripcionEstado',
			            PC.idPrecioCotizacion, PC.precioCotizacion,
			            CASE WHEN C.idEstado = 1
					        THEN 1 ELSE 0 END 'canEdit',
			            CASE WHEN C.idEstado = 1
					        THEN 1 ELSE 0 END 'canDelete',
			            CASE WHEN C.idEstado = 1
					              AND ISNULL(TRIM(C.descripcion),'') <> ''
					              AND PC.precioCotizacion > 0
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
		                LEFT JOIN PrecioCotizacion PC  ON(C.idCotizacion = PC.idCotizacion 
						                                   AND PC.fecha = (SELECT MAX(fecha) 
										        		                   FROM PrecioCotizacion 
																           WHERE idCotizacion = C.idCotizacion))
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

        private class Validator : AbstractValidator<GetCotizacionPaginadoQuery>
        {
            public Validator()
            {
                RuleFor(x => x.Pages)
                    .GreaterThan(0).WithMessage("La cantidad de paginas debe ser un entero positivo");
                RuleFor(x => x.Rows)
                    .GreaterThan(0).WithMessage("La cantidad de registros debe ser un entero positivo");
            }
        }
    }
}