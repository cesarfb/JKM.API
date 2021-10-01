using JKM.PERSISTENCE.Utils;
using Microsoft.AspNetCore.Http;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace JKM.PERSISTENCE.GlobalErrorHandling
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                ErrorModel error = new ErrorModel();
                error.Message = "Ocurrio un error";
                
                HttpResponse response = context.Response;
                response.ContentType = "application/json";

                switch (exception)
                {
                    case ArgumentNullException e://204 - Servicio aceptado pero resultado nulo;
                        response.StatusCode = (int)HttpStatusCode.NoContent;
                        error.Exception = "Content Exception";
                        error.Message = "No se encontraron resultados";
                        break;
                    case ArgumentException e://422 - Error en las validaciones
                        response.StatusCode = (int)HttpStatusCode.UnprocessableEntity;
                        error.Exception = "Validations Exception";
                        error.Message = exception.Message;
                        break;
                    case SqlException e://502 - Error en la aplicacion (SQL)
                        response.StatusCode = (int)HttpStatusCode.BadGateway;
                        error.Exception = "Sql Exception";
                        error.Data = e.Message;
                        break;
                    case DBConcurrencyException e://502 - Error en la aplicacion(QUERY)
                        response.StatusCode = (int)HttpStatusCode.BadGateway;
                        error.Exception = "Query Exception";
                        error.Data = e.Message;
                        break;
                    default: //500 - unhandled error
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        error.Message = exception.Message;
                        error.Exception = "Not Handler Exception";
                        break;
                }

                var result = JsonSerializer.Serialize(error);
                await response.WriteAsync(result);
            }
        }
    }
    public class AppException : Exception
    {
        public AppException() : base() { }

        public AppException(string message) : base(message) { }

        public AppException(string message, params object[] args)
            : base(String.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }
}