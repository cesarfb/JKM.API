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
			string sql = $@"SELECT P.idProyecto, P.nombre as nombreProyecto, MIN(AP.fechaInicio) as fechaInicio, 
								MAX(AP.fechaFin) as fechaFin, P.descripcion,
								EP.idEstado, EP.descripcion 'DescripcionEstado',
								V.precio 'Precio', V.idCotizacion,
								CLI.razonSocial, CLI.ruc, CLI.idCliente
							FROM Proyecto P
							INNER JOIN EstadoProyecto EP ON (EP.idEstado = P.idEstado)
							INNER JOIN ProyectoVenta PV ON (PV.idProyecto=P.idProyecto)
							INNER JOIN Venta V ON (V.idVenta = PV.idVenta)
							INNER JOIN Cotizacion C ON (C.idCotizacion = V.idCotizacion)
							LEFT JOIN ActividadProyecto AP ON(AP.idCotizacion =  C.idCotizacion)
							INNER JOIN Cliente CLI ON (CLI.idCliente = C.idCliente)
							WHERE P.idProyecto = {request.IdProyecto}
							group by P.idProyecto, P.nombre, P.descripcion,
								EP.idEstado, EP.descripcion,
								V.precio, V.idCotizacion,
								CLI.razonSocial, CLI.ruc, CLI.idCliente;";

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
