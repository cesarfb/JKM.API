using Dapper;
using JKM.APPLICATION.Aggregates;
using JKM.UTILITY.Utils;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JKM.APPLICATION.Queries.Usuario.GetUsuarioPaginado
{
    public class GetUsuarioPaginadoQueryHandler : IRequestHandler<GetUsuarioPaginadoQuery, PaginadoResponse<UsuarioModel>>
    {
        private readonly IDbConnection _conexion;

        public GetUsuarioPaginadoQueryHandler(IDbConnection conexion)
        {
            _conexion = conexion;
        }

        public async Task<PaginadoResponse<UsuarioModel>> Handle(GetUsuarioPaginadoQuery request, CancellationToken cancellationToken)
        {

            string sql = $@"SELECT COUNT(1) FROM Usuario;";

            sql += $@"select
                        U.idUsuario,
                    	U.username,
                    	U.password,
                    	DU.nombre,
                    	DU.apellido,
                    	DU.fechaNacimiento,
                    	R.descripcion as descripcionRol,
                        EU.descripcion as descripcionEstado
                    from 
                    	Usuario U inner join
                    	DetalleUsuario DU on DU.idDetalleUsuario = U.idDetalleUsuario inner join
                    	Rol R on R.idRol = U.idRol inner join
                        EstadoUsuario EU on EU.idEstado = U.idEstado";

            using (IDbConnection connection = _conexion)
            {
                try
                {
                    PaginadoResponse<UsuarioModel> newPaginado = new PaginadoResponse<UsuarioModel>();

                    connection.Open();
                    using (var multi = await connection.QueryMultipleAsync(sql, request))
                    {
                        newPaginado.TotalRows = multi.ReadFirst<int>();
                        newPaginado.Data = multi.Read<UsuarioModel>().AsList();
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
