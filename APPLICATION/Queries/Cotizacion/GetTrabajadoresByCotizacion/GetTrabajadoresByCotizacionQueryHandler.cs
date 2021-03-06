using Dapper;
using JKM.APPLICATION.Aggregates;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace JKM.APPLICATION.Queries.Cotizacion.GetTrabajadoresByCotizacion
{
    public class GetTrabajadoresByCotizacionQueryHandler : IRequestHandler<GetTrabajadoresByCotizacionQuery, IEnumerable<TipoTrabajadorModel>>
    {
        private readonly IDbConnection _conexion;
        public GetTrabajadoresByCotizacionQueryHandler(IDbConnection conexion)
        {
            _conexion = conexion;
        }

        public async Task<IEnumerable<TipoTrabajadorModel>> Handle(GetTrabajadoresByCotizacionQuery request, CancellationToken cancellationToken)
        {
            string sql = $@"SELECT TTC.idCotizacion, TTC.precio, TTC.cantidad,
			                    TT.idTipoTrabajador, TT.nombre as 'descripcion'
			                FROM TipoTrabajadorCotizacion TTC
			                INNER JOIN TipoTrabajador TT ON TT.idTipoTrabajador = TTC.idTipoTrabajador
			                WHERE idCotizacion = {request.IdCotizacion}";

            using (IDbConnection connection = _conexion)
            {
                try
                {
                    connection.Open();
                    IEnumerable<TipoTrabajadorModel> trabajadores =
                        await connection.QueryAsync<TipoTrabajadorModel>(sql);

                    connection.Close();

                    return trabajadores;
                }
                catch (SqlException err)
                {
                    throw err;
                }
            }
        }
    }
}
