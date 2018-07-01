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

        public static SqlDataReader query(string request)
        {
            /**
             * query method will be used to query database, returns rows matching request
             * **/
             try
            {
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conn"].ToString());
                conn.Open();
                SqlCommand command = new SqlCommand(request, conn);
                System.Diagnostics.Debug.WriteLine(request);
                return command.ExecuteReader();
            } catch(InvalidOperationException e)
            {
                System.Diagnostics.Debug.WriteLine(e);
            }
            catch (SqlException e)
            {
                System.Diagnostics.Debug.WriteLine(e);
            }
            catch (ConfigurationErrorsException e)
            {
                System.Diagnostics.Debug.WriteLine(e);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
            }
            System.Diagnostics.Debug.WriteLine(request);
            return null;
        }
    }
}