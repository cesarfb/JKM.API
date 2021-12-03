using Dapper;
using JKM.APPLICATION.Aggregates;
using MediatR;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace JKM.APPLICATION.Queries.Trabajador.GetTrabajadorById
{
	public class GetTrabajadorByIdQueryHandler : IRequestHandler<GetTrabajadorByIdQuery, TrabajadorModel>
	{
		private readonly IDbConnection _conexion;
		public GetTrabajadorByIdQueryHandler(IDbConnection conexion)
		{
			_conexion = conexion;
		}

		public async Task<TrabajadorModel> Handle(GetTrabajadorByIdQuery request, CancellationToken cancellationToken)
		{
			string sql = $@"SELECT T.idTrabajador, DU.nombre, DU.apellido, DU.fechaNacimiento,
								TT.idTipoTrabajador, TT.descripcion 'DescripcionTipo', TT.precioReferencial,  TT.nombre 'nombreTipo',
								ET.idEstado, ET.descripcion 'DescripcionEstado'
							FROM Trabajador T
							INNER JOIN DetalleUsuario DU ON (DU.idDetalleUsuario = T.idDetalleUsuario)
							INNER JOIN TipoTrabajador TT ON (TT.idTipoTrabajador = T.idTipoTrabajador)
							INNER JOIN EstadoTrabajador ET ON (ET.idEstado = T.idEstado)
							WHERE idTrabajador = {request.IdTrabajador}";

			using (IDbConnection connection = _conexion)
			{
				try
				{
					connection.Open();

					TrabajadorModel trabajador =
						await connection.QueryFirstOrDefaultAsync<TrabajadorModel>(sql);

					connection.Close();

					if (trabajador == null) throw new ArgumentNullException();

					return trabajador;
				}
				catch (SqlException err)
				{
					throw err;
				}
			}
		}
	}
}
