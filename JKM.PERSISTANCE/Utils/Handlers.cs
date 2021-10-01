using System;
using System.Data;
using FluentValidation.Results;

namespace JKM.PERSISTENCE.Utils
{
    public static class Handlers
    {
       public static void HandlerException(ValidationResult result)
        {
            if (!result.IsValid)
            {
                foreach (ValidationFailure error in result.Errors)
                {
                    throw new ArgumentException(error.ErrorMessage);
                }
            }
        }
        
        public static void ExceptionClose(IDbConnection connection, IDbTransaction transaction = null, string msg = "")
        {
            if (transaction != null) 
                transaction.Rollback();
            if(connection.State != ConnectionState.Closed)
                connection.Close();
            throw new DBConcurrencyException(msg);
        }

        public static ResponseModel CloseConnection(IDbConnection connection, IDbTransaction transaction = null, string msg = "")
        {
            if (transaction != null)
                transaction.Commit();
            if (connection.State != ConnectionState.Closed)
                connection.Close();

            ResponseModel response = new ResponseModel();
            response.Message = msg;

            return response;
        }
    }
}
