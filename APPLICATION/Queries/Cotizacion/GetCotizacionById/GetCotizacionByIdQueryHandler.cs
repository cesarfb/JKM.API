using Dapper;
using JKM.APPLICATION.Aggregates;
using MediatR;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace JKM.APPLICATION.Queries.Cotizacion.GetCotizacionById
{
    public class GetCotizacionByIdQueryHandler : IRequestHandler<GetCotizacionByIdQuery, CotizacionModel>
    {
        private readonly IDbConnection _conexion;
        public GetCotizacionByIdQueryHandler(IDbConnection conexion)
        {
            _conexion = conexion;
        }

        public async Task<CotizacionModel> Handle(GetCotizacionByIdQuery request, CancellationToken cancellationToken)
        {
            string sql = $@"SELECT C.idCotizacion, C.solicitante, C.descripcion, C.fechaSolicitud,
	                            C.descripcion, C.email, CLI.idCliente, CLI.razonSocial,
	                            EC.idEstado, EC.descripcion as 'descripcionEstado',
	                            C.precioCotizacion, TIPO.idTipoCotizacion, TIPO.descripcion as 'descripcionTipoCotizacion'
                            FROM Cotizacion C 
                            INNER JOIN EstadoCotizacion EC  ON(C.idEstado = EC.idEstado)
                            INNER JOIN Cliente CLI on(CLI.idCliente = C.idCliente)
                            INNER JOIN TipoCotizacion TIPO on(TIPO.idTipoCotizacion=C.idTipoCotizacion)
		                    WHERE C.idCotizacion = {request.IdCotizacion}";

            using (IDbConnection connection = _conexion)
            {
                try
                {
                    connection.Open();
                    CotizacionModel cotizacion =
                        await connection.QueryFirstOrDefaultAsync<CotizacionModel>(sql);
                    connection.Close();

                    if (cotizacion == null) throw new ArgumentNullException();

                    return cotizacion;
                }
                catch (SqlException err)
                {
                    throw new Exception("Ocurrio un error al traer los registros", err);
                }
            }
        }
    }
}
