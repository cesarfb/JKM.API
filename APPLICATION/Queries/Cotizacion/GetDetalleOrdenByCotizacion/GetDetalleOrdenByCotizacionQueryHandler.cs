using Dapper;
using JKM.APPLICATION.Aggregates;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace JKM.APPLICATION.Queries.Cotizacion.GetDetalleOrdenByCotizacion
{
    public class GetDetalleOrdenByCotizacionQueryHandler : IRequestHandler<GetDetalleOrdenByCotizacionQuery, IEnumerable<DetalleOrdenModel>>
    {
        private readonly IDbConnection _conexion;
        public GetDetalleOrdenByCotizacionQueryHandler(IDbConnection conexion)
        {
            _conexion = conexion;
        }

        public async Task<IEnumerable<DetalleOrdenModel>> Handle(GetDetalleOrdenByCotizacionQuery request, CancellationToken cancellationToken)
        {
            string sql = $@"select idDetalleOrden, idCotizacion, PRO.idProducto, 
		                            cantidad, precio,
		                            PRO.codigo as 'codigoProducto', PRO.nombre as 'nombreProducto', PRO.imagen
                            from DetalleOrden DET
                            INNER JOIN Producto PRO on(PRO.idProducto=DET.idProducto) 
			                WHERE idCotizacion = {request.IdCotizacion}";

            using (IDbConnection connection = _conexion)
            {
                try
                {
                    connection.Open();
                    IEnumerable<DetalleOrdenModel> detalleOrden =
                        await connection.QueryAsync<DetalleOrdenModel>(sql);

                    connection.Close();

                    return detalleOrden;
                }
                catch (SqlException err)
                {
                    throw err;
                }
            }
        }
    }
}
