using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace FMS.App_Code
{
    public class Util
    {
        /**
         *  Util class will have static utility methods 
         * **/

        public class Conn
        {
            
        }

        public static SqlDataReader query(string request)
        {
            /**
             * query method will be used to query database, returns rows matching request
             * **/
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conn"].ToString());
            conn.Open();
            SqlCommand command = new SqlCommand(request, conn);
            return command.ExecuteReader();
        }
    }
}