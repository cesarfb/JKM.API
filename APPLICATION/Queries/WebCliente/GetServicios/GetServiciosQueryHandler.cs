using Dapper;
using JKM.APPLICATION.Aggregates;
using MediatR;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace JKM.APPLICATION.Queries.WebCliente.GetServicios
{
    public class GetServiciosQueryHandler : IRequestHandler<GetServiciosQuery, IEnumerable<ServicioWebModel>>
    {
        private readonly IDbConnection _conexion;
        public GetServiciosQueryHandler(IDbConnection conexion)
        {
            _conexion = conexion;
        }

        public async Task<IEnumerable<ServicioWebModel>> Handle(GetServiciosQuery request, CancellationToken cancellationToken)
        {
            string sql = $@"SELECT S.idServicio, S.nombre, S.imagen, S.descripcion
		                    FROM Servicio S
                            WHERE S.isActive = 1";
            using (IDbConnection connection = _conexion)
            {

                try
                {
                    connection.Open();

                    IEnumerable<ServicioWebModel> servicios =
                        await connection.QueryAsync<ServicioWebModel>(sql);

                    connection.Close();

                    return servicios;
                }
                catch (SqlException err)
                {
                    throw err;
                }
            }
        }
    }
}
