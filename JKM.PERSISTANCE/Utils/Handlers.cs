using System.Data;
using System.Transactions;

namespace JKM.PERSISTENCE.Utils
{
    public static class Handlers
    {
        public static void ExceptionClose(IDbConnection connection, string msg = "")
        {
            if(connection.State != ConnectionState.Closed)
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
    }
}
