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

namespace JKM.APPLICATION.Queries.Venta.GetCuotasVentaById
{
    public class GetCuotasVentaByIdQueryHandler : IRequestHandler<GetCuotasVentaByIdQuery, IEnumerable<VentaCuotasModel>>
    {
        private readonly IDbConnection _conexion;
        public GetCuotasVentaByIdQueryHandler(IDbConnection conexion)
        {
            _conexion = conexion;
        }

        public async Task<IEnumerable<VentaCuotasModel>> Handle(GetCuotasVentaByIdQuery request, CancellationToken cancellationToken)
        {
            ValidationResult validator = new Validator().Validate(request);
            Handlers.HandlerException(validator);

            string sql = $@"SELECT DV.idDetalleVenta, DV.numeroCuota, 
			                    DV.pagoParcial, DV.idVenta, DV.fechaCuota
		                    FROM DetalleVenta DV
		                    WHERE DV.idVenta = {request.IdVenta}";

            using (IDbConnection connection = _conexion)
            {
                try
                {
                    connection.Open();

                    IEnumerable<VentaCuotasModel> ventaModel =
                        await connection.QueryAsync<VentaCuotasModel>(sql);

                    connection.Close();

                    if (ventaModel.AsList().Count == 0) throw new ArgumentNullException();

                    return ventaModel;
                }
                catch (SqlException err)
                {
                    throw err;
                }
            }
        }

        private class Validator : AbstractValidator<GetCuotasVentaByIdQuery>
        {
            public Validator()
            {
                RuleFor(x => x.IdVenta)
                    .GreaterThan(0).WithMessage("El idVenta debe ser un entero positivo");
            }
        }
    }
}
