using Dapper;
using JKM.APPLICATION.Aggregates;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace JKM.APPLICATION.Queries.Proyecto.GetTrabajadoresByProyecto
{
	public class GetTrabajadoresByProyectoQueryHandler : IRequestHandler<GetTrabajadoresByProyectoQuery, IEnumerable<TrabajadorProyectoModel>>
	{
		private readonly IDbConnection _conexion;
		public GetTrabajadoresByProyectoQueryHandler(IDbConnection conexion)
		{
			_conexion = conexion;
		}

		public async Task<IEnumerable<TrabajadorProyectoModel>> Handle(GetTrabajadoresByProyectoQuery request, CancellationToken cancellationToken)
		{
			string sql = $@"SELECT T.idTrabajador, TT.idTipoTrabajador, TT.descripcion 'descripcionTipo',
								DU.nombre, DU.apellido, DU.fechaNacimiento
							FROM TrabajadorProyecto TP
							INNER JOIN Trabajador T ON T.idTrabajador = TP.idTrabajador
							INNER JOIN DetalleUsuario DU ON DU.idDetalleUsuario = T.idDetalleUsuario
							INNER JOIN TipoTrabajador TT ON TT.idTipoTrabajador = T.idTipoTrabajador
							WHERE TP.idProyecto = {request.IdProyecto}
							ORDER BY TT.idTipoTrabajador DESC";

			using (IDbConnection connection = _conexion)
			{
				try
				{
					connection.Open();

					IEnumerable<TrabajadorProyectoModel> trabajadorProyecto =
						await connection.QueryAsync<TrabajadorProyectoModel>(sql);

					connection.Close();

					if (trabajadorProyecto.AsList().Count == 0) throw new ArgumentNullException();

					return trabajadorProyecto;
				}
				catch (SqlException err)
				{
					throw err;
				}
			}
		}
	}
}