using Dapper;
using JKM.APPLICATION.Aggregates;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace JKM.APPLICATION.Queries.Trabajador.GetTrabajadorDisponibleProyecto
{
    public class GetTrabajadorDisponibleProyectoQueryHandler : IRequestHandler<GetTrabajadorDisponibleProyectoQuery, IEnumerable<TrabajadorModel>>
    {
        private readonly IDbConnection _conexion;
        public GetTrabajadorDisponibleProyectoQueryHandler(IDbConnection conexion)
        {
            _conexion = conexion;
        }

        public async Task<IEnumerable<TrabajadorModel>> Handle(GetTrabajadorDisponibleProyectoQuery request, CancellationToken cancellationToken)
        {
            string sql = $@"SELECT T.idTrabajador, T.nombre, T.apellidoPaterno, T.apellidoMaterno, T.fechaNacimiento,
		                            ET.idEstado, ET.descripcion 'descripcionEstado',
		                            TT.idTipoTrabajador, TT.descripcion 'descripcionTipo', TT.precioReferencial, TT.nombre 'nombreTipo'
                            FROM Trabajador T 
                            INNER JOIN EstadoTrabajador ET ON ET.idEstado = T.idEstado
                            INNER JOIN TipoTrabajador TT ON TT.idTipoTrabajador = T.idTipoTrabajador
                            LEFT JOIN ProyectoTrabajador PT ON PT.idTrabajador = T.idTrabajador
                            WHERE PT.idTrabajador is null";

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
