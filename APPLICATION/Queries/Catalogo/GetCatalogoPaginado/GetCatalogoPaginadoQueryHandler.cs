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

namespace JKM.APPLICATION.Queries.Catalogo.GetCatalogoPaginado
{
    public class GetCatalogoPaginadoQueryHandler : IRequestHandler<GetCatalogoPaginadoQuery, PaginadoResponse<CatalogoModel>>
    {
        private readonly IDbConnection _conexion;

        public GetCatalogoPaginadoQueryHandler(IDbConnection conexion)
        {
            _conexion = conexion;
        }

        public async Task<PaginadoResponse<CatalogoModel>> Handle(GetCatalogoPaginadoQuery request, CancellationToken cancellationToken)
        {

            string sql = $@"SELECT COUNT(1) FROM Catalogo;";

            sql += $@"select
		                C.idCatalogo,
		                P.nombre,
		                P.imagen,
		                C.precio,
		                C.stock,
                        C.isActive
	                  from 
		                Catalogo C inner join 
		                Producto P on P.idProducto = C.idProducto";

            using (IDbConnection connection = _conexion)
            {
                try
                {
                    PaginadoResponse<CatalogoModel> newPaginado = new PaginadoResponse<CatalogoModel>();

                    connection.Open();
                    using (var multi = await connection.QueryMultipleAsync(sql, request))
                    {
                        newPaginado.TotalRows = multi.ReadFirst<int>();
                        newPaginado.Data = multi.Read<CatalogoModel>().AsList();
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
