using Dapper;
using JKM.APPLICATION.Aggregates;
using JKM.UTILITY.Utils;
using MediatR;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace JKM.APPLICATION.Queries.Venta.GetVentaPaginado
{
    public class GetVentaPaginadoQueryHandler : IRequestHandler<GetVentaPaginadoQuery, PaginadoResponse<VentaModel>>
    {
        private readonly IDbConnection _conexion;
        public GetVentaPaginadoQueryHandler(IDbConnection conexion)
        {
            _conexion = conexion;
        }

        public async Task<PaginadoResponse<VentaModel>> Handle(GetVentaPaginadoQuery request, CancellationToken cancellationToken)
        {
            string sql = $@"SELECT COUNT(1) FROM Venta;";

            sql += $@"SELECT V.idVenta, V.precio, V.fechaRegistro, 
                        TV.idTipo, TV.descripcion 'TipoDescripcion', 
                        EV.idEstado, EV.descripcion 'EstadoDescripcion',
                        Cli.razonSocial, Cli.ruc
                    FROM Venta V
                    	INNER JOIN EstadoVenta	EV	ON (EV.idEstado		= V.idEstado)
                    	INNER JOIN TipoVenta	TV	ON (C.idTipo		= V.idTipo)
                    	INNER JOIN Cotizacion	Cot	ON (Cot.idCotizacion	= V.idCotizacion)
                    	INNER JOIN Cliente		Cli   ON (Cli.idCliente		= Cot.idCliente)
                    ORDER BY V.idVenta DESC";

            using (IDbConnection connection = _conexion)
            {
                try
                {
                    PaginadoResponse<VentaModel> newPaginado = new PaginadoResponse<VentaModel>();

                    connection.Open();
                    using (var multi = await connection.QueryMultipleAsync(sql, request))
                    {
                        newPaginado.TotalRows = multi.ReadFirst<int>();
                        newPaginado.Data = multi.Read<VentaModel>().AsList();
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
