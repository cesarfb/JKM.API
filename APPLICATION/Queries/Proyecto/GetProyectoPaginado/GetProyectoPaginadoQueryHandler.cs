using Dapper;
using JKM.APPLICATION.Aggregates;
using JKM.UTILITY.Utils;
using MediatR;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace JKM.APPLICATION.Queries.Proyecto.GetProyectoPaginado
{
    public class GetProyectoPaginadoQueryHandler : IRequestHandler<GetProyectoPaginadoQuery, PaginadoResponse<ProyectoModel>>
    {
        private readonly IDbConnection _conexion;
        public GetProyectoPaginadoQueryHandler(IDbConnection conexion)
        {
            _conexion = conexion;
        }

        public async Task<PaginadoResponse<ProyectoModel>> Handle(GetProyectoPaginadoQuery request, CancellationToken cancellationToken)
        {
            string sql = $@"SELECT COUNT(1) FROM Proyecto;";

            sql += $@"SELECT 
	                    P.idProyecto, P.nombre as nombreProyecto, MIN(AP.fechaInicio) as fechaInicio, 
	                    MAX(AP.fechaFin) as fechaFin, P.descripcion, EP.idEstado, 
	                    EP.descripcion 'DescripcionEstado',
	                    convert(decimal(15,2),convert(decimal,SUM(isnull(AP_FINALIZADAS.peso,0)))/convert(decimal,SUM(isnull(AP.peso,0))))*100 as PorcentajeTareasFinalizadas
                    FROM Proyecto P
                    INNER JOIN EstadoProyecto EP ON (EP.idEstado = P.idEstado)
                    INNER JOIN ProyectoVenta PV ON (PV.idProyecto=P.idProyecto)
                    INNER JOIN Venta V ON (V.idVenta = PV.idVenta)
                    INNER JOIN Cotizacion C ON (C.idCotizacion = V.idCotizacion and V.idTipo=1)
                    LEFT JOIN ActividadProyecto AP ON(AP.idCotizacion =  C.idCotizacion)
                    LEFT JOIN ActividadProyecto AP_FINALIZADAS ON(AP_FINALIZADAS.idActividad =  AP.idActividad AND AP_FINALIZADAS.idEstado=3)
                    GROUP BY P.idProyecto, P.nombre, P.descripcion, 
	                    EP.descripcion, EP.idEstado
                   
                    ORDER BY P.idProyecto DESC;";

            using (IDbConnection connection = _conexion)
            {
                try
                {
                    PaginadoResponse<ProyectoModel> newPaginado = new PaginadoResponse<ProyectoModel>();

                    connection.Open();
                    using (var multi = await connection.QueryMultipleAsync(sql))
                    {
                        newPaginado.TotalRows = multi.ReadFirst<int>();
                        newPaginado.Data = multi.Read<ProyectoModel>().AsList();
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