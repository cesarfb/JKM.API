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

namespace JKM.APPLICATION.Queries.Proyecto.GetActividadesByProyecto
{
    public class GetActividadesByProyectoQueryHandler : IRequestHandler<GetActividadesByProyectoQuery, IEnumerable<ActividadProyectoTreeNode>>
    {
        private readonly IDbConnection _conexion;
        public GetActividadesByProyectoQueryHandler(IDbConnection conexion)
        {
            _conexion = conexion;
        }

        public async Task<IEnumerable<ActividadProyectoTreeNode>> Handle(GetActividadesByProyectoQuery request, CancellationToken cancellationToken)
        {
            string sql = $@"SELECT AP.idActividad, AP.descripcion, AP.fechaInicio, AP.fechaFin,
								AP.peso, AP.idPadre, AP.idHermano,
								EA.idEstado, EA.descripcion 'descripcionEstado'
							FROM ActividadProyecto AP
							INNER JOIN EstadoActividad EA ON EA.idEstado = AP.idEstado
							INNER JOIN Cotizacion C ON C.idCotizacion = AP.idCotizacion
							INNER JOIN Venta V ON V.idCotizacion = C.idCotizacion 
							INNER JOIN ProyectoVenta PV ON PV.idVenta = V.idVenta
							WHERE PV.idProyecto = {request.IdProyecto};";

            using (IDbConnection connection = _conexion)
            {
                try
                {
                    connection.Open();

                    IEnumerable<ActividadProyectoModel> actividades =
                        await connection.QueryAsync<ActividadProyectoModel>(sql);

                    connection.Close();

                    //FORMATEARLO
                    IEnumerable<ActividadProyectoTreeNode> actividadModel = (from actividadPadre in actividades
                                                                                where (actividadPadre.IdPadre == null)
                                                                                select new ActividadProyectoTreeNode
                                                                                {
                                                                                    data = new ActividadProyectoModel()
                                                                                    {
                                                                                        IdActividad = actividadPadre.IdActividad,
                                                                                        Descripcion = actividadPadre.Descripcion,
                                                                                        FechaInicio= actividadPadre.FechaInicio,
                                                                                        FechaFin=actividadPadre.FechaFin,
                                                                                        Peso = actividadPadre.Peso,
                                                                                        IdPadre = actividadPadre.IdPadre,
                                                                                        IdHermano = actividadPadre.IdHermano,
                                                                                        IdEstado = actividadPadre.IdEstado,
                                                                                        DescripcionEstado = actividadPadre.DescripcionEstado,
                                                                                        Profundidad = 1
                                                                                    },
                                                                                    children = (from actividadPrimerHijo in actividades
                                                                                                where (actividadPrimerHijo.IdPadre == actividadPadre.IdActividad)
                                                                                                select new ActividadProyectoTreeNode
                                                                                                {
                                                                                                    data = new ActividadProyectoModel()
                                                                                                    {
                                                                                                        IdActividad = actividadPrimerHijo.IdActividad,
                                                                                                        Descripcion = actividadPrimerHijo.Descripcion,
                                                                                                        Peso = actividadPrimerHijo.Peso,
                                                                                                        FechaInicio = actividadPrimerHijo.FechaInicio,
                                                                                                        FechaFin = actividadPrimerHijo.FechaFin,
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
