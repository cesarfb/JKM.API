using Dapper;
using JKM.APPLICATION.Aggregates;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace JKM.APPLICATION.Queries.Trabajador.GetTipoTrabajador
{
    public class GetTipoTrabajadorQueryHandler : IRequestHandler<GetTipoTrabajadorQuery, IEnumerable<TipoTrabajador>>
    {
        private readonly IDbConnection _conexion;
        public GetTipoTrabajadorQueryHandler(IDbConnection conexion)
        {
            _conexion = conexion;
        }

        public async Task<IEnumerable<TipoTrabajador>> Handle(GetTipoTrabajadorQuery request, CancellationToken cancellationToken)
        {
			string sql = $@"SELECT idTipoTrabajador 'id', descripcion, precioReferencial, nombre
							FROM TipoTrabajador";

			using (IDbConnection connection = _conexion)
			{
				try
				{
					connection.Open();

					IEnumerable<TipoTrabajador> tipo =
						await connection.QueryAsync<TipoTrabajador>(sql);

					connection.Close();

					if (tipo.AsList().Count == 0) throw new ArgumentNullException();

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