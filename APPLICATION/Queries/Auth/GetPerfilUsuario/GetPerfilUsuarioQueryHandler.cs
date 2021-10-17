using Dapper;
using JKM.APPLICATION.Aggregates;
using MediatR;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace JKM.APPLICATION.Queries.Auth.GetPerfilUsuario
{
    public class GetPerfilUsuarioQueryHandler : IRequestHandler<GetPerfilUsuarioQuery, PerfilModel>
    {
        private readonly IDbConnection _conexion;
        public GetPerfilUsuarioQueryHandler(IDbConnection conexion)
        {
            _conexion = conexion;
        }
        public async Task<PerfilModel> Handle(GetPerfilUsuarioQuery request, CancellationToken cancellationToken)
        {
            string sql = $@"SELECT DU.nombre, DU.apellido, DU.fechaNacimiento, 
                                    R.idRol, R.descripcion
                            FROM Usuario U 
                            INNER JOIN Rol R ON R.idRol = U.idRol
                            INNER JOIN DetalleUsuario DU ON U.idDetalleUsuario = DU.idDetalleUsuario
		                    WHERE U.idUsuario = @IdUsuario;";

            using (IDbConnection connection = _conexion)
            {
                try
                {
                    connection.Open();

                    PerfilModel perfil = await connection.QueryFirstOrDefaultAsync<PerfilModel>(sql, request);

                    connection.Close();

                    if (perfil == null) throw new ArgumentNullException();

                    return perfil;
                }
                catch (SqlException err)
                {
                    throw err;
                }
            }
        }
    }
}
