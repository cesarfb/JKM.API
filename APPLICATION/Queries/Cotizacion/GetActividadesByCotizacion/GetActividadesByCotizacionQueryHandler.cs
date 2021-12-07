using Dapper;
using FluentValidation;
using JKM.APPLICATION.Aggregates;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace JKM.APPLICATION.Queries.Cotizacion.GetActividadesByCotizacion
{
    public class GetActividadesByCotizacionQueryHandler : IRequestHandler<GetActividadesByCotizacionQuery, IEnumerable<ActividadCotizancionTreeNode>>
    {
        private readonly IDbConnection _conexion;
        public GetActividadesByCotizacionQueryHandler(IDbConnection conexion)
        {
            _conexion = conexion;
        }

        public async Task<IEnumerable<ActividadCotizancionTreeNode>> Handle(GetActividadesByCotizacionQuery request, CancellationToken cancellationToken)
        {
            string sql = $@"SELECT AP.idActividad, AP.descripcion, AP.fechaInicio, AP.fechaFin,
		                        AP.peso, AP.idPadre, AP.idHermano,
		                        EA.idEstado, EA.descripcion 'descripcionEstado'
		                    FROM ActividadProyecto AP
		                    INNER JOIN EstadoActividad EA ON EA.idEstado = AP.idEstado
		                    WHERE idCotizacion = {request.IdCotizacion};";

            using (IDbConnection connection = _conexion)
            {
                try
                {
                    connection.Open();

                    IEnumerable<ActividadCotizacionModel> actividades =
                        await connection.QueryAsync<ActividadCotizacionModel>(sql);

                    connection.Close();

                    //FORMATEARLO
                    IEnumerable<ActividadCotizancionTreeNode> actividadModel = (from actividadPadre in actividades
                                                                            where (actividadPadre.IdPadre == null)
                                                                            select new ActividadCotizancionTreeNode
                                                                            {
                                                                                data = new ActividadCotizacionModel()
                                                                                {
                                                                                    IdActividad = actividadPadre.IdActividad,
                                                                                    Descripcion = actividadPadre.Descripcion,
                                                                                    Peso = actividadPadre.Peso,
                                                                                    IdPadre = actividadPadre.IdPadre,
                                                                                    IdHermano = actividadPadre.IdHermano,
                                                                                    IdEstado = actividadPadre.IdEstado,
                                                                                    DescripcionEstado = actividadPadre.DescripcionEstado,
                                                                                    Profundidad = 1
                                                                                },
                                                                                children = (from actividadPrimerHijo in actividades
                                                                                        where (actividadPrimerHijo.IdPadre == actividadPadre.IdActividad)
                                                                                        select new ActividadCotizancionTreeNode
                                                                                        {
                                                                                            data = new ActividadCotizacionModel()
                                                                                            {
                                                                                                IdActividad = actividadPrimerHijo.IdActividad,
                                                                                                Descripcion = actividadPrimerHijo.Descripcion,
                                                                                                Peso = actividadPrimerHijo.Peso,
                                                                                                IdPadre = actividadPrimerHijo.IdPadre,
                                                                                                IdHermano = actividadPrimerHijo.IdHermano,
                                                                                                IdEstado = actividadPrimerHijo.IdEstado,
                                                                                                DescripcionEstado = actividadPrimerHijo.DescripcionEstado,
                                                                                                Profundidad = 2
                                                                                            }
                                                                                        })
                                                                            });

                    if (actividadModel.Count() == 0) throw new ArgumentNullException();

                    return actividadModel;
                }
                catch (SqlException err)
                {
                    throw err;
                }
            }
        }
    }
}
