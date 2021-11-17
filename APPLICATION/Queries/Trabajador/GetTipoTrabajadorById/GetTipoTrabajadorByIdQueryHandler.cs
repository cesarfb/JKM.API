using Dapper;
using JKM.APPLICATION.Aggregates;
using MediatR;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace JKM.APPLICATION.Queries.Trabajador.GetTipoTrabajadorById
{
    public class GetTipoTrabajadorByIdQueryHandler : IRequestHandler<GetTipoTrabajadorByIdQuery, TipoTrabajador>
	{
		private readonly IDbConnection _conexion;
		public GetTipoTrabajadorByIdQueryHandler(IDbConnection conexion)
		{
			_conexion = conexion;
		}

		public async Task<TipoTrabajador> Handle(GetTipoTrabajadorByIdQuery request, CancellationToken cancellationToken)
		{
			string sql = $@"SELECT idTipoTrabajador 'id', descripcion, precioReferencial, nombre
							FROM TipoTrabajador
							WHERE idTipoTrabajador={request.IdTrabajador}";

			using (IDbConnection connection = _conexion)
			{
				try
				{
					connection.Open();

					TipoTrabajador tipo =
						await connection.QueryFirstOrDefaultAsync<TipoTrabajador>(sql);

					connection.Close();

					if (tipo == null) throw new ArgumentNullException();

					return tipo;
				}
				catch (SqlException err)
				{
					throw err;
				}
			}
		}
	}
}
