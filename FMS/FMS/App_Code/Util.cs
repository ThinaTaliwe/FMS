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
		public static double distance(double[] from, double[] to) {
            try {
                double lon1, lat1, lon2, lat2;
                lon1 = from[0];
                lat1 = from[1];
                lon2 = to[0];
                lat2 = to[1];
                var r = 6371000.0;
                var phi1 = toRad(lat1);
                var phi2 = toRad(lat2);
                var deltaPhi = toRad(lat2 - lat1);
                var deltaLambda = toRad(lon2 - lon1);
                var a = Math.Pow(Math.Sin(deltaPhi / 2.0), 2) + Math.Cos(phi1) * Math.Cos(phi2) * Math.Pow(Math.Sin(deltaLambda / 2.0), 2);
                var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
                return r * c;
            } catch (Exception ex) {
                System.Diagnostics.Debug.WriteLine(ex);
            }
            return 0;
		}

        public static double toRad(double angle) {
            return Math.PI * angle / 180;
        }

        public static double[] getCoords(String coords) {
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
