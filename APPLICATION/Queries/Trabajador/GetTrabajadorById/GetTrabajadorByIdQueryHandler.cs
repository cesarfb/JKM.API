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
			ValidationResult validator = new Validator().Validate(request);
			Handlers.HandlerException(validator);

			string sql = $@"SELECT T.idTrabajador, DU.nombre, DU.apellido, DU.fechaNacimiento,
								TT.idTipoTrabajador, TT.descripcion 'DescripcionTipo', TT.precioReferencial,
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

		private class Validator : AbstractValidator<GetTrabajadorByIdQuery>
		{
			public Validator()
			{
				RuleFor(x => x.IdTrabajador)
					.GreaterThan(0).WithMessage("El IdTrabajador debe ser un entero positivo");
			}
		}
	}
}
