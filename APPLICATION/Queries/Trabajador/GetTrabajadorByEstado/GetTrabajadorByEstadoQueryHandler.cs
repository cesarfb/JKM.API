using Dapper;
using JKM.APPLICATION.Aggregates;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace JKM.APPLICATION.Queries.Trabajador.GetTrabajadorByEstado
{
    public class GetTrabajadorByEstadoQueryHandler : IRequestHandler<GetTrabajadorByEstadoQuery, IEnumerable<TrabajadorModel>>
    {
        private readonly IDbConnection _conexion;
        public GetTrabajadorByEstadoQueryHandler(IDbConnection conexion)
        {
            _conexion = conexion;
        }

        public async Task<IEnumerable<TrabajadorModel>> Handle(GetTrabajadorByEstadoQuery request, CancellationToken cancellationToken)
        {
            string sql = $@"SELECT T.idTrabajador, DU.nombre, DU.apellido, DU.fechaNacimiento, 
								TT.idTipoTrabajador, TT.descripcion 'DescripcionTipo', TT.precioReferencial,
								ET.idEstado, ET.descripcion 'DescripcionEstado'
							FROM Trabajador T
							INNER JOIN DetalleUsuario DU ON (DU.idDetalleUsuario = T.idDetalleUsuario)
							INNER JOIN TipoTrabajador TT ON (TT.idTipoTrabajador = T.idTipoTrabajador)
							INNER JOIN EstadoTrabajador ET ON (ET.idEstado = T.idEstado)
							WHERE T.idEstado = {request.IdEstado}";

            using (IDbConnection connection = _conexion)
            {
                try
                {
                    connection.Open();
                    IEnumerable<TrabajadorModel> response =
                                await connection.QueryAsync<TrabajadorModel>(sql);
                    connection.Close();

                    if (response.AsList().Count == 0) throw new ArgumentNullException();

                    return response;
                }
                catch (SqlException err)
                {
                    throw err;
                }
            }
        }
    }
}
