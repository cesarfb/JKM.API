using Dapper;
using JKM.APPLICATION.Aggregates;
using MediatR;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace JKM.APPLICATION.Queries.WebCliente.GetProductos
{
    public class GetCatalogoQueryHandler : IRequestHandler<GetCatalogoQuery, IEnumerable<CatalogoWebModel>>
    {
        private readonly IDbConnection _conexion;
        public GetCatalogoQueryHandler(IDbConnection conexion)
        {
            _conexion = conexion;
        }

        public async Task<IEnumerable<CatalogoWebModel>> Handle(GetCatalogoQuery request, CancellationToken cancellationToken)
        {
            string sql = $@"SELECT C.idCatalogo, C.precio, C.stock,
                            P.nombre, P.codigo, P.imagen
		                    FROM Catalogo C
                            INNER JOIN Producto P ON P.idProducto = C.idProducto
                            WHERE C.isActive = 1";
            using (IDbConnection connection = _conexion)
            {

                try
                {
                    connection.Open();

                    IEnumerable<CatalogoWebModel> catalogo =
                        await connection.QueryAsync<CatalogoWebModel>(sql);

                    connection.Close();

                    return catalogo;
                }
                catch (SqlException err)
                {
                    throw err;
                }
            }
        }
    }
}