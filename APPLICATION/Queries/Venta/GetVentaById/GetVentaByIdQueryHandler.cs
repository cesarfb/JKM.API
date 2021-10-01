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

namespace JKM.APPLICATION.Queries.Venta.GetVentaById
{
    public class GetVentaByIdQueryHandler : IRequestHandler<GetVentaByIdQuery, VentaModel>
    {
        private readonly IDbConnection _conexion;
        public GetVentaByIdQueryHandler(IDbConnection conexion)
        {
            _conexion = conexion;
        }

        public async Task<VentaModel> Handle(GetVentaByIdQuery request, CancellationToken cancellationToken)
        {
            ValidationResult validator = new Validator().Validate(request);
            Handlers.HandlerException(validator);

            string sql = $@"SELECT V.idVenta, V.precioTotal, V.fechaRegistro,
			                    TV.idTipo, TV.descripcion 'TipoDescripcion', 
			                    EV.idEstado, EV.descripcion 'EstadoDescripcion',
			                    C.razonSocial, C.ruc
			                FROM Venta V
			                INNER JOIN EstadoVenta EV ON (EV.idEstado = V.idEstado)
			                INNER JOIN TipoVenta TV ON (TV.idTipo = V.idTipo)
			                INNER JOIN Cliente C ON (C.idCliente = V.idCliente)
		                    WHERE V.idVenta = {request.IdVenta}";

            using (IDbConnection connection = _conexion)
            {
                try
                {
                    connection.Open();

                    VentaModel ventaModel =
                        await connection.QueryFirstOrDefaultAsync<VentaModel>(sql);

                    connection.Close();

                    if (ventaModel == null) throw new ArgumentNullException();

                    return ventaModel;
                }
                catch (SqlException err)
                {
                    throw err;
                }
            }
        }

        private class Validator : AbstractValidator<GetVentaByIdQuery>
        {
            public Validator()
            {
                RuleFor(x => x.IdVenta)
                    .GreaterThan(0).WithMessage("El idVenta debe ser un entero positivo");
            }
        }
    }
}
