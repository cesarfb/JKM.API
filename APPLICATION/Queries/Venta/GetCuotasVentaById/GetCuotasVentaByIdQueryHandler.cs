using Dapper;
using JKM.APPLICATION.Aggregates;
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
    }
}