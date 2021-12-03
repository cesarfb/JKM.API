using Dapper;
using JKM.APPLICATION.Aggregates;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JKM.APPLICATION.Queries.Servicio.GetServicioById
{
    public class GetServicioByIdQueryHandler : IRequestHandler<GetServicioByIdQuery, ServicioModel>
    {
        private readonly IDbConnection _conexion;

        public GetServicioByIdQueryHandler(IDbConnection conexion)
        {
            _conexion = conexion;
        }

        public async Task<ServicioModel> Handle(GetServicioByIdQuery request, CancellationToken cancellationToken)
        {
            string sql = $@"select
		                       idServicio,
                               nombre,
                               imagen,
                               descripcion,
                               isActive
	                       from 
		                       Servicio
	                       where 
		                       idServicio = {request.IdServicio}";

            using (IDbConnection connection = _conexion)
            {
                try
                {
                    connection.Open();

                    ServicioModel servicioModel = await connection.QueryFirstOrDefaultAsync<ServicioModel>(sql);

                    connection.Close();

                    if (servicioModel == null) throw new ArgumentNullException();

                    return servicioModel;
                }

                catch (SqlException err)
                {
                    throw err;
                }
            }

        }
    }
}
