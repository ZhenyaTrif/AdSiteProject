using DataAccess.Core;
using System.Configuration;
using System.Data.SqlClient;

namespace DataAccess.Wrappers
{
    public class BaseConnection
    {
        public static SqlHelper sqlHelper = new SqlHelper(ConfigurationManager.ConnectionStrings["AdProjectDb"].ToString());
        public SqlConnection connection;

        public BaseConnection() { }

        public void CloseConnection()
        {
            sqlHelper.CloseConnection(connection);
        }
    }
}
