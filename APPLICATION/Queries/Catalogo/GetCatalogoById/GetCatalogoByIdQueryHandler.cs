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

namespace JKM.APPLICATION.Queries.Catalogo.GetCatalogoById
{
    public class GetCatalogoByIdQueryHandler : IRequestHandler<GetCatalogoByIdQuery, CatalogoModel>
    {
        private readonly IDbConnection _conexion;

        public GetCatalogoByIdQueryHandler(IDbConnection conexion)
        {
            _conexion = conexion;
        }

        public async Task<CatalogoModel> Handle(GetCatalogoByIdQuery request, CancellationToken cancellationToken)
        {
                string sql = $@"select
		                            C.idCatalogo,
		                            P.idProducto,
		                            P.imagen,
		                            C.precio,
		                            C.stock
	                            from 
		                            Catalogo C inner join 
		                            Producto P on P.idProducto = C.idProducto
	                            where 
		                            C.idCatalogo = {request.IdCatalogo}";

            using (IDbConnection connection = _conexion)
            {
                try
                {
                    connection.Open();

                    CatalogoModel catalogoModel = await connection.QueryFirstOrDefaultAsync<CatalogoModel>(sql);

                    connection.Close();

                    if (catalogoModel == null) throw new ArgumentNullException();

                    return catalogoModel;
                }

                catch (SqlException err)
                {
                    throw err;
                }
            }

        }
    }
}
