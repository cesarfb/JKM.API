using Dapper;
using JKM.APPLICATION.Aggregates;
using MediatR;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace JKM.APPLICATION.Queries.Proyecto.GetProyectoById
{
    public class GetProyectoByIdQueryHandler : IRequestHandler<GetProyectoByIdQuery, ProyectoModel>
    {
        private readonly IDbConnection _conexion;
        public GetProyectoByIdQueryHandler(IDbConnection conexion)
        {
            _conexion = conexion;
        }

        public async Task<ProyectoModel> Handle(GetProyectoByIdQuery request, CancellationToken cancellationToken)
        {
			string sql = $@"SELECT P.idProyecto, P.nombreProyecto, P.fechaInicio, P.fechaFin, P.descripcion,
								EP.idEstado, EP.descripcion 'DescripcionEstado',
								V.precioTotal 'Precio', V.idCotizacion,
								C.razonSocial, C.ruc, C.idCliente
							FROM Proyecto P
							INNER JOIN EstadoProyecto EP ON (EP.idEstado = P.idEstado)
							INNER JOIN Venta V ON (V.idVenta = P.idVenta)
							INNER JOIN Cliente C ON (C.idCliente = V.idCliente)
							WHERE P.idProyecto = {request.IdProyecto};";

			using (IDbConnection connection = _conexion)
			{
				try
				{
					connection.Open();

					ProyectoModel proyecto =
						await connection.QueryFirstOrDefaultAsync<ProyectoModel>(sql);

					connection.Close();

					if (proyecto == null) throw new ArgumentNullException();

					return proyecto;
				}
				catch (SqlException err)
				{
					throw err;
				}
			}
		}
    }
}
