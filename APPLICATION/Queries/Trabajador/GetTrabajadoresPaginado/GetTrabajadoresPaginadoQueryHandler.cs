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
            ValidationResult validator = new Validator().Validate(request);
            Handlers.HandlerException(validator);

            string sql = $@"SELECT COUNT(1) 
                            FROM Trabajador T
                            WHERE {request.Estado} = (CASE WHEN {request.Estado} <> 0 THEN
							    T.idEstado ELSE 0 END)
							    AND {request.Tipo}  = (CASE WHEN {request.Tipo} <> 0 THEN
								T.idTipoTrabajador ELSE 0 END);";

			sql += $@"SELECT T.idTrabajador, DU.nombre, DU.apellido, DU.fechaNacimiento,
								T.idEstado, E.descripcion 'descripcionEstado',
								TT.idTipoTrabajador, TT.descripcion 'descripcionTipo', TT.precioReferencial
					  FROM Trabajador T 
					  INNER JOIN DetalleUsuario DU ON DU.idDetalleUsuario = T.idDetalleUsuario
					  INNER JOIN TipoDetalle TD ON TD.idTipoDetalle = DU.idTipoDetalle
					  INNER JOIN EstadoTrabajador E ON E.idEstado = T.idEstado
					  INNER JOIN TipoTrabajador TT ON TT.idTipoTrabajador = T.idTipoTrabajador
					  WHERE {request.Estado} = (CASE WHEN {request.Estado} <> 0 
                                                THEN T.idEstado ELSE 0 END)
							AND {request.Tipo}  = (CASE WHEN {request.Tipo} <> 0 THEN
												   T.idTipoTrabajador ELSE 0 END)
					  ORDER BY T.idTrabajador DESC
					  OFFSET (({request.Pages} - 1) * {request.Rows}) ROWS FETCH NEXT {request.Rows} ROWS ONLY;";

			using (IDbConnection connection = _conexion)
			{
                try
                {
                    PaginadoResponse<TrabajadorModel> newPaginado = new PaginadoResponse<TrabajadorModel>();

                    connection.Open();
                    using (var multi = await connection.QueryMultipleAsync(sql))
                    {
                        newPaginado.TotalRows = multi.ReadFirst<int>();
                        newPaginado.Data = multi.Read<TrabajadorModel>().AsList();
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

        private class Validator : AbstractValidator<GetTrabajadoresPaginadoQuery>
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
