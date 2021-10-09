using Dapper;
using FluentValidation;
using JKM.APPLICATION.Aggregates;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace JKM.APPLICATION.Queries.Proyecto.GetActividadesByProyecto
{
	public class GetActividadesByProyectoQueryHandler : IRequestHandler<GetActividadesByProyectoQuery, IEnumerable<ActividadProyectoModel>>
	{
		private readonly IDbConnection _conexion;
		public GetActividadesByProyectoQueryHandler(IDbConnection conexion)
		{
			_conexion = conexion;
		}

		public async Task<IEnumerable<ActividadProyectoModel>> Handle(GetActividadesByProyectoQuery request, CancellationToken cancellationToken)
		{
			string sql = $@"SELECT AP.idActividad, AP.descripcion, AP.fechaInicio, AP.fechaFin,
								AP.peso, AP.idPadre, AP.idHermano,
								EA.idEstado, EA.descripcion 'descripcionEstado'
							FROM ActividadProyecto AP
							INNER JOIN EstadoActividad EA ON EA.idEstado = AP.idEstado
							WHERE idProyecto = {request.IdProyecto};";

			using (IDbConnection connection = _conexion)
			{
				try
				{
					connection.Open();

					IEnumerable<ActividadProyectoModel> actividades =
						await connection.QueryAsync<ActividadProyectoModel>(sql);

					connection.Close();

					//FORMATEARLO
					IEnumerable<ActividadProyectoModel> actividadModel = (from actividadPadre in actividades
																		  where (actividadPadre.IdPadre == 0)
																		  select new ActividadProyectoModel
																		  {
																			  IdActividad = actividadPadre.IdActividad,
																			  Descripcion = actividadPadre.Descripcion,
																			  FechaInicio = actividadPadre.FechaInicio,
																			  FechaFin = actividadPadre.FechaFin,
																			  Peso = actividadPadre.Peso,
																			  IdPadre = actividadPadre.IdPadre,
																			  IdHermano = actividadPadre.IdHermano,
																			  IdEstado = actividadPadre.IdEstado,
																			  DescripcionEstado = actividadPadre.DescripcionEstado,
																			  Hijo = (from actividadHijo in actividades
																					  where (actividadHijo.IdPadre == actividadPadre.IdActividad)
																					  select new ActividadProyectoModel
																					  {
																						  IdActividad = actividadHijo.IdActividad,
																						  Descripcion = actividadHijo.Descripcion,
																						  FechaInicio = actividadHijo.FechaInicio,
																						  FechaFin = actividadHijo.FechaFin,
																						  Peso = actividadHijo.Peso,
																						  IdPadre = actividadHijo.IdPadre,
																						  IdHermano = actividadHijo.IdHermano,
																						  IdEstado = actividadHijo.IdEstado,
																						  DescripcionEstado = actividadHijo.DescripcionEstado,
																					  })
																		  });

					if (actividadModel.Count() == 0) throw new ArgumentNullException();

					return actividadModel;
				}
				catch (SqlException err)
				{
					throw err;
				}
			}
		}
	}
}
