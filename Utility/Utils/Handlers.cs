using System;
using System.Data;
using System.Transactions;

namespace JKM.UTILITY.Utils
{
    public static class Handlers
    {
        public static bool BeAValidDate(string value, DateTime? min = null, DateTime? max = null)
        {
            DateTime date;
            bool valid = DateTime.TryParse(value, out date);
            if (!valid) return false;

            DateTime newDate = DateTime.Parse(value);

            if (min != null)
            {
                DateTime compare = min ?? DateTime.Now;
                int isValid = DateTime.Compare(newDate, compare);
                if (isValid < 0) return false;
            }
            if (min != null)
            {
                DateTime compare = max ?? DateTime.Now;
                int isValid = DateTime.Compare(newDate, compare);
                if (isValid > 0) return false;
            }
            return true;
        }

        public static void ExceptionClose(IDbConnection connection, string msg = "")
        {
            if (connection.State != ConnectionState.Closed)
                connection.Close();
            throw new DBConcurrencyException(msg);
        }

        public static ResponseModel CloseConnection(IDbConnection connection, TransactionScope transaction = null, string msg = "")
        {
            if (transaction != null)
                transaction.Complete();
            if (connection.State != ConnectionState.Closed)
                connection.Close();

            ResponseModel response = new ResponseModel();
            response.Message = msg;

            return response;
        }

        public static ResponseModel CloseConnection(IDbConnection connection, TransactionScope transaction = null, string msg = "", Object data = null)
        {
            if (transaction != null)
                transaction.Complete();
            if (connection.State != ConnectionState.Closed)
                connection.Close();

            ResponseModel response = new ResponseModel();
            response.Message = msg;
            response.Data = data;

            return response;
        }
    }
}
