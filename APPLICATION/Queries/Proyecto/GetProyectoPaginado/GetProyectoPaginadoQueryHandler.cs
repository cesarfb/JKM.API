﻿using Dapper;
using FluentValidation;
using FluentValidation.Results;
using JKM.APPLICATION.Aggregates;
using JKM.PERSISTENCE.Utils;
using MediatR;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace JKM.APPLICATION.Queries.Proyecto.GetProyectoPaginado
{
    public class GetProyectoPaginadoQueryHandler : IRequestHandler<GetProyectoPaginadoQuery, PaginadoResponse<ProyectoModel>>
    {
        private readonly IDbConnection _conexion;
        public GetProyectoPaginadoQueryHandler(IDbConnection conexion)
        {
            _conexion = conexion;
        }

        public async Task<PaginadoResponse<ProyectoModel>> Handle(GetProyectoPaginadoQuery request, CancellationToken cancellationToken)
        {
            ValidationResult validator = new Validator().Validate(request);
            Handlers.HandlerException(validator);

            string sql = $@"SELECT COUNT(1) FROM Proyecto;";

            sql += $@"SELECT P.idProyecto, P.nombreProyecto, P.fechaInicio, 
						P.fechaFin, P.descripcion, EP.idEstado, EP.descripcion 'DescripcionEstado'
					  FROM Proyecto P
					  INNER JOIN EstadoProyecto EP ON (EP.idEstado = P.idEstado)
					  ORDER BY P.idProyecto DESC
					  OFFSET (({request.Pages}-1) * {request.Rows}) ROWS FETCH NEXT {request.Rows} ROWS ONLY;";

            using (IDbConnection connection = _conexion)
            {
                try
                {
                    PaginadoResponse<ProyectoModel> newPaginado = new PaginadoResponse<ProyectoModel>();

                    connection.Open();
                    using (var multi = await connection.QueryMultipleAsync(sql))
                    {
                        newPaginado.TotalRows = multi.ReadFirst<int>();
                        newPaginado.Data = multi.Read<ProyectoModel>().AsList();
                        newPaginado.TotalPages = Math.Ceiling(newPaginado.TotalRows / request.Rows);
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

        private class Validator : AbstractValidator<GetProyectoPaginadoQuery>
        {
            public Validator()
            {
                RuleFor(x => x.Pages)
                    .GreaterThan(0).WithMessage("La cantidad de paginas debe ser un entero positivo");
                RuleFor(x => x.Rows)
                    .GreaterThan(0).WithMessage("La cantidad de paginas debe ser un entero positivo");
            }
        }
    }
}