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

namespace JKM.APPLICATION.Queries.Cotizacion.GetCotizacionById
{
    public class GetCotizacionByIdQueryHandler : IRequestHandler<GetCotizacionByIdQuery, CotizacionModel>
    {
        private readonly IDbConnection _conexion;
        public GetCotizacionByIdQueryHandler(IDbConnection conexion)
        {
            _conexion = conexion;
        }

        public async Task<CotizacionModel> Handle(GetCotizacionByIdQuery request, CancellationToken cancellationToken)
        {
            ValidationResult validator = new Validator().Validate(request);
            Handlers.HandlerException(validator);

            string sql = $@"SELECT C.idCotizacion, C.solicitante, C.descripcion, C.fechaSolicitud,
			                    C.descripcion, C.email, C.empresa,
			                    EC.idEstado, EC.descripcion as 'descripcionEstado',
			                    PC.idPrecioCotizacion, PC.precioCotizacion
		                    FROM Cotizacion C 
		                    INNER JOIN EstadoCotizacion EC  ON(C.idEstado = EC.idEstado)
		                    LEFT JOIN PrecioCotizacion PC  ON(C.idCotizacion = PC.idCotizacion 
											                    AND PC.fecha = (SELECT MAX(fecha) 
																                    FROM PrecioCotizacion 
																                    WHERE idCotizacion = C.idCotizacion))
		                    WHERE C.idCotizacion = {request.IdCotizacion}";

            using (IDbConnection connection = _conexion)
            {
                try
                {
                    connection.Open();
                    CotizacionModel cotizacion =
                        await connection.QueryFirstOrDefaultAsync<CotizacionModel>(sql);
                    connection.Close();

                    if (cotizacion == null) throw new ArgumentNullException();

                    return cotizacion;
                }
                catch (SqlException err)
                {
                    throw new Exception("Ocurrio un error al traer los registros", err);
                }
            }
        }

        private class Validator : AbstractValidator<GetCotizacionByIdQuery>
        {
            public Validator()
            {
                RuleFor(x => x.IdCotizacion)
                    .GreaterThan(0).WithMessage("El IdCotizacion debe ser un entero positivo");
            }
        }
    }
}
