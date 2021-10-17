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

            //sql += $@"SELECT V.idVenta, V.precioTotal, V.fechaRegistro,
		          //       TV.idTipo, TV.descripcion 'TipoDescripcion', 
		          //       EV.idEstado, EV.descripcion 'EstadoDescripcion',
		          //       C.razonSocial, C.ruc
		          //   FROM Venta V
		          //   INNER JOIN EstadoVenta EV ON (EV.idEstado = V.idEstado)
		          //   INNER JOIN TipoVenta TV ON (TV.idTipo = V.idTipo)
		          //   INNER JOIN Cliente C ON (C.idCliente = V.idCliente)
		          //   ORDER BY V.idVenta DESC
		          //   OFFSET (({request.Pages} - 1) * {request.Rows}) ROWS FETCH NEXT {request.Rows} ROWS ONLY;";

            sql += $@"select 
                    	A.idVenta, 
                    	A.precio, 
                    	A.fechaRegistro,
                        C.idTipo, 
                    	C.descripcion 'TipoDescripcion', 
                        B.idEstado, 
                    	B.descripcion 'EstadoDescripcion',
                        E.razonSocial,
                    	E.ruc
                    from Venta A
                    	inner join EstadoVenta	B	on (B.idEstado		= A.idEstado)
                    	inner join TipoVenta	C	on (C.idTipo		= A.idTipo)
                    	inner join Cotizacion	D	on (D.idCotizacion	= A.idCotizacion)
                    	inner join Cliente		E   on (E.idCliente		= D.idCliente)
                    order by
                    	A.idVenta desc";

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
                        newPaginado.TotalPages = Math.Ceiling(newPaginado.TotalRows / request.Rows);
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
