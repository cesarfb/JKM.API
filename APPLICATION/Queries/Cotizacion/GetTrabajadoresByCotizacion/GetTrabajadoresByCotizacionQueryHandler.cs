using Dapper;
using FluentValidation;
using FluentValidation.Results;
using JKM.APPLICATION.Aggregates;
using JKM.PERSISTENCE.Utils;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace JKM.APPLICATION.Queries.Cotizacion.GetTrabajadoresByCotizacion
{
    public class GetTrabajadoresByCotizacionQueryHandler : IRequestHandler<GetTrabajadoresByCotizacionQuery, IEnumerable<TipoTrabajadorModel>>
    {
        private readonly IDbConnection _conexion;
        public GetTrabajadoresByCotizacionQueryHandler(IDbConnection conexion)
        {
            _conexion = conexion;
        }

        public async Task<IEnumerable<TipoTrabajadorModel>> Handle(GetTrabajadoresByCotizacionQuery request, CancellationToken cancellationToken)
        {
            ValidationResult validator = new Validator().Validate(request);
            Handlers.HandlerException(validator);

            string sql = $@"SELECT TTC.idTipoTrabajadorCotizacion, TTC.idCotizacion, TTC.precio, TTC.cantidad,
			                    TT.idTipoTrabajador, TT.descripcion
			                FROM TipoTrabajadorCotizacion TTC
			                INNER JOIN TipoTrabajador TT ON TT.idTipoTrabajador = TTC.idTipoTrabajador
			                WHERE idCotizacion = {request.IdCotizacion}";

            using (IDbConnection connection = _conexion)
            {
                try
                {
                    connection.Open();
                    IEnumerable<TipoTrabajadorModel> cotizacion =
                        await connection.QueryAsync<TipoTrabajadorModel>(sql);

                    connection.Close();

                    if (cotizacion.AsList().Count == 0) throw new ArgumentNullException();

                    return cotizacion;
                }
                catch (SqlException err)
                {
                    throw err;
                }
            }
        }

        private class Validator : AbstractValidator<GetTrabajadoresByCotizacionQuery>
        {
            public Validator()
            {
                RuleFor(x => x.IdCotizacion)
                    .GreaterThan(0).WithMessage("El IdCotizacion debe ser un entero positivo");
            }
        }
    }
}
