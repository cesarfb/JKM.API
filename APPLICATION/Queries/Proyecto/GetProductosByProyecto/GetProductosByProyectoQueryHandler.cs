using Dapper;
using JKM.APPLICATION.Aggregates;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace JKM.APPLICATION.Queries.Proyecto.GetProductosByProyecto
{
    public class GetProductosByProyectoQueryHandler : IRequestHandler<GetProductosByProyectoQuery, IEnumerable<DetalleOrdenModel>>
    {
        private readonly IDbConnection _conexion;
        public GetProductosByProyectoQueryHandler(IDbConnection conexion)
        {
            _conexion = conexion;
        }

        public async Task<IEnumerable<DetalleOrdenModel>> Handle(GetProductosByProyectoQuery request, CancellationToken cancellationToken)
        {
            string sql = $@"select idDetalleOrden, DET.idCotizacion, PRO.idProducto, 
		                            cantidad, DET.precio,
		                            PRO.codigo as 'codigoProducto', PRO.nombre as 'nombreProducto', PRO.imagen
                            from DetalleOrden DET
                            INNER JOIN Producto PRO on(PRO.idProducto=DET.idProducto) 
							INNER JOIN Venta V on V.idCotizacion = DET.idCotizacion
							INNER JOIN ProyectoVenta PV on PV.idVenta=V.idVenta
			                WHERE PV.idProyecto = {request.IdProyecto}";

            using (IDbConnection connection = _conexion)
            {
                try
                {
                    connection.Open();
                    IEnumerable<DetalleOrdenModel> detalleOrden =
                        await connection.QueryAsync<DetalleOrdenModel>(sql);

                    connection.Close();

                    if (detalleOrden.AsList().Count == 0) throw new ArgumentNullException();

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
