using Dapper;
using JKM.APPLICATION.Aggregates;
using JKM.UTILITY.Utils;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace JKM.APPLICATION.Queries.Venta.GetCuotasVentaById
{
    public class GetCuotasVentaByIdQueryHandler : IRequestHandler<GetCuotasVentaByIdQuery, PaginadoResponse<VentaCuotasModel>>
    {
        private readonly IDbConnection _conexion;
        public GetCuotasVentaByIdQueryHandler(IDbConnection conexion)
        {
            _conexion = conexion;
        }

        public async Task<PaginadoResponse<VentaCuotasModel>> Handle(GetCuotasVentaByIdQuery request, CancellationToken cancellationToken)
        {
            string sql = $@"SELECT COUNT(1) FROM DetalleVenta;";

            sql += $@"SELECT 
                        DV.idDetalleVenta, DV.numeroCuota, 
			            DV.precio as PagoParcial, DV.idVenta, DV.fecha as FechaCuota
		              FROM DetalleVenta DV
		              WHERE DV.idVenta = {request.IdVenta}";

            using (IDbConnection connection = _conexion)
            {
                try
                {
                    PaginadoResponse<VentaCuotasModel> newPaginado = new PaginadoResponse<VentaCuotasModel>();

                    connection.Open();

                    using (var multi = await connection.QueryMultipleAsync(sql, request))
                    {
                        newPaginado.TotalRows = multi.ReadFirst<int>();
                        newPaginado.Data = multi.Read<VentaCuotasModel>().AsList();
                    }
                    connection.Close();

                    if (newPaginado.Data.AsList().Count == 0) throw new ArgumentNullException();

                    return newPaginado;

                }
                catch (SqlException err)
                {
                    throw err;
                }


            }
        }
    }
}