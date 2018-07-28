using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using Newtonsoft.Json.Linq;

namespace FMS.App_Code
{
    public class Util
    {

        public static string getRoutePoints(string route)
        {
            List<string> info = new List<string>();
            string result = "";
            try
            {
                JObject obj = JObject.Parse(route);
                JToken legs = obj["routes"][0]["legs"][0];                
                info.Add(legs["distance"]["text"].ToString());
                var steps = legs["steps"];
                string text = "";
                string polyline = "";
                foreach(var step in steps) {
                    text = step["distance"]["text"].ToString();
                    text += "#" + step["html_instructions"];
                    text += "#" + step["polyline"]["points"];
                    info.Add(text);
                    text = "";
                }
                foreach(var data in info)  result += pad(data) + " ";
                result += polyline;
                return result;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
            }
            return result;
        }

        public static string pad(string text) { return text.Replace(' ', '_'); }

        public static string getRoute(double[] from, double[] to)
        {
            try
            {
                String link = "https://maps.googleapis.com/maps/api/directions/json?mode=driving&origin=";
                link += from[0].ToString().Replace(',', '.') + "," + from[1].ToString().Replace(',', '.') + "&destination=";
                link += to[0].ToString().Replace(',', '.') + "," + to[1].ToString().Replace(',', '.') + "&key=AIzaSyChZ0yP0HTxPypmlDNYgkpQMXqQD3UASpw";
                WebClient wc = new WebClient();
                System.Diagnostics.Debug.WriteLine(link);
                return wc.DownloadString(link);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
            return null;
        }

        public static double distance(double[] from, double[] to) {
            try {
                double lon1, lat1, lon2, lat2;
                lat1 = from[0];
                lon1 = from[1];
                lat2 = to[0];
                lon2 = to[1];
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
                String[] parts = coords.Replace('.', ',').Split(':');
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
            } 
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                try {
                    System.Diagnostics.Debug.WriteLine("Retrying query: " + request);
                    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conn2"].ToString());
                    conn.Open();
                    SqlCommand command = new SqlCommand(request, conn);
                    return command.ExecuteReader();
                } catch(Exception ex) {
                    System.Diagnostics.Debug.Write(ex);
                }

            }
            return null;
        }
    }
}