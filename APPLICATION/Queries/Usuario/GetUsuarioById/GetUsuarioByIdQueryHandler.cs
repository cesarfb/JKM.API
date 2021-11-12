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

namespace JKM.APPLICATION.Queries.Usuario.GetUsuarioById
{
    public class GetUsuarioByIdQueryHandler : IRequestHandler<GetUsuarioByIdQuery, UsuarioModel>
    {
        private readonly IDbConnection _conexion;

        public GetUsuarioByIdQueryHandler(IDbConnection conexion)
        {
            _conexion = conexion;
        }

        public async Task<UsuarioModel> Handle(GetUsuarioByIdQuery request, CancellationToken cancellationToken)
        {
            string sql = $@"select 
                            	DU.nombre,
                            	DU.apellido,
                            	U.username,
                            	U.password,
                            	DU.fechaNacimiento,
                            	R.descripcion
                            from 
                            	Usuario U inner join
                            	DetalleUsuario DU on DU.idDetalleUsuario = U.idDetalleUsuario inner join
                            	Rol R on R.idRol = U.idRol
                            where 
                            	U.idUsuario = {request.IdUsuario}";

            using (IDbConnection connection = _conexion)
            {
                try
                {
                    connection.Open();

                    UsuarioModel usuarioModel =
                        await connection.QueryFirstOrDefaultAsync<UsuarioModel>(sql);

                    connection.Close();

                    if (usuarioModel == null) throw new ArgumentNullException();

                    return usuarioModel;
                }
                catch (SqlException err)
                {
                    throw err;
                }
            }



        }
    }
}
