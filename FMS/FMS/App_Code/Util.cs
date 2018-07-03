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

        public double[] getCoords(String coords) {
            try {
                String[] parts = coords.Split(':');
                return new double[] { Double.Parse(parts[0]), Double.Parse(parts[1]) };
            } catch (Exception ex) {
                System.Diagnostics.Debug.WriteLine("invalid coords given " + coords + ex);
            }
            return null;
        }

        public static SqlDataReader query(string request)
        {
             try
            {
                System.Diagnostics.Debug.WriteLine(request);
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conn"].ToString());
                conn.Open();
                SqlCommand command = new SqlCommand(request, conn);
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
            return null;
        }
    }
}