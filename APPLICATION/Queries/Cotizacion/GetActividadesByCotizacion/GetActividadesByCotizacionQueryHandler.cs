using Dapper;
using FluentValidation;
using FluentValidation.Results;
using JKM.APPLICATION.Aggregates;
using JKM.PERSISTENCE.Utils;
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
    public class GetActividadesByCotizacionQueryHandler : IRequestHandler<GetActividadesByCotizacionQuery, IEnumerable<ActividadCotizacionModel>>
    {
        private readonly IDbConnection _conexion;
        public GetActividadesByCotizacionQueryHandler(IDbConnection conexion)
        {
            _conexion = conexion;
        }

        public async Task<IEnumerable<ActividadCotizacionModel>> Handle(GetActividadesByCotizacionQuery request, CancellationToken cancellationToken)
        {
            ValidationResult validator = new Validator().Validate(request);
            Handlers.HandlerException(validator);

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
                    IEnumerable<ActividadCotizacionModel> actividadModel = (from actividadPadre in actividades
                                                                            where (actividadPadre.IdPadre == 0)
                                                                            select new ActividadCotizacionModel
                                                                            {
                                                                                IdActividad = actividadPadre.IdActividad,
                                                                                Descripcion = actividadPadre.Descripcion,
                                                                                Peso = actividadPadre.Peso,
                                                                                IdPadre = actividadPadre.IdPadre,
                                                                                IdHermano = actividadPadre.IdHermano,
                                                                                IdEstado = actividadPadre.IdEstado,
                                                                                DescripcionEstado = actividadPadre.DescripcionEstado,
                                                                                Hijo = (from actividadHijo in actividades
                                                                                        where (actividadHijo.IdPadre == actividadPadre.IdActividad)
                                                                                        select new ActividadCotizacionModel
                                                                                        {
                                                                                            IdActividad = actividadHijo.IdActividad,
                                                                                            Descripcion = actividadHijo.Descripcion,
                                                                                            Peso = actividadHijo.Peso,
                                                                                            IdPadre = actividadHijo.IdPadre,
                                                                                            IdHermano = actividadHijo.IdHermano,
                                                                                            IdEstado = actividadHijo.IdEstado,
                                                                                            DescripcionEstado = actividadHijo.DescripcionEstado,
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

        private class Validator : AbstractValidator<GetActividadesByCotizacionQuery>
        {
            public Validator()
            {
                RuleFor(x => x.IdCotizacion)
                    .GreaterThan(0).WithMessage("El IdCotizacion debe ser un entero positivo");
            }
        }
    }
}
