using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using Newtonsoft.Json.Linq;
using System.Web.UI;
using System.Net.Sockets;

namespace FMS.App_Code
{
    public class Util
    {
        public static string key = "AIzaSyC9H32t06ZsT32y0EPOHAAUH0A42gIHq6E";

        public static JObject averageSpeed(List<string> coords, List<DateTime> times) {
            JObject json = new JObject();
            if (coords.Capacity == times.Capacity)
            {
                double distance = 0, hours = 0;
                string currentCoords, prevCoords = coords[0];
                DateTime currentTime, prevTime = times[0];
                JArray marks = new JArray();
                var overSpeed = 0;
                for (int c = 1; c < coords.Count; c++) {
                    JToken jsonMark = new JObject();
                    currentCoords = coords[c];
                    currentTime = times[c];
                    var disChange = Util.distance(getCoords(prevCoords), getCoords(currentCoords));
                    var timeChange = currentTime.Subtract(prevTime).TotalSeconds / 3600.0;
                    distance += disChange;
                    hours += timeChange;
                    jsonMark["distance"] = distance;
                    jsonMark["time"] = currentTime;
                    jsonMark["coords"] = currentCoords;
                    var speed = disChange / timeChange;
                    jsonMark["speed"] = speed;
                    if (speed > 80)
                        overSpeed++;
                    marks.Add(jsonMark);
                    prevCoords = currentCoords;
                    prevTime = currentTime;
                }
                json["distance"] = Math.Round(distance, 2);
                json["speed"] = Math.Round(distance / hours, 2);
                json["time"] = Math.Round(hours, 2);
                json["info"] = marks;
                json["overspeed_ratio"] = Math.Round((Convert.ToDouble(overSpeed) / coords.Count) * 100, 2);
            }
            else
                return null;
            return json;
        }

        public static double totalDistance(List<string> coords) {
            double[] current;
            string prev = "";
            double total = 0;
            foreach(var coord in coords) {
                if (coord == prev)
                    continue;
                else {
                   try {
                        current = getCoords(coord);
                        double dis = distance(current, getCoords(prev));
                        Util.print(dis.ToString() + " " + prev + " " + coord);
                        total += dis;
                        prev = coord;
                   } catch (Exception ex) {
                        Util.print(ex.ToString() + "invalid coords: " + coord);
                   } 
                }
            }
            return total;
        }

        public static string getLatLong(string address) {
            try {
                string link = "https://maps.googleapis.com/maps/api/geocode/json?address=" + address.Replace(' ', '+') + "&key=" + key;
                string result = readLink(link);
                JObject obj = JObject.Parse(result);
                var results = obj["results"][0];
                if(results != null) {
                    var loc = results["geometry"]["location"];
                    return loc["lat"] + ":" + loc["lng"];
                }
            } catch(Exception ex) {
                System.Diagnostics.Debug.WriteLine(ex);
            } return DriverHandle.INTERNAL_ERROR;
        }

        public static string getAddress(double[] coords) {
            try {
                string link = "https://maps.googleapis.com/maps/api/geocode/json?latlng=";
                link += coords[0].ToString().Replace(',', '.') + "," + coords[1].ToString().Replace(',', '.') + "&key=" + key;
                string result = readLink(link);
                JObject obj = JObject.Parse(result);
                var addr = obj["results"][0];
                if(addr != null) {
                    return addr["formatted_address"].ToString();
                } 
            } catch (Exception ex) {
                System.Diagnostics.Debug.WriteLine(ex);
            } return DriverHandle.INTERNAL_ERROR;
        }

        public static string getRoutePoints(string route)
        {
            JObject json = new JObject();
            try
            {
                JObject obj = JObject.Parse(route);
                if (obj["geocoded_waypoints"][0]["geocoder_status"].ToString().Contains("OK"))
                {
                    JToken legs = obj["routes"][0]["legs"][0];
                    json["distance"] = legs["distance"]["text"].ToString();
                    var steps = legs["steps"];
                    string text = "";
                    JToken token;
                    JArray jsonSteps = new JArray();
                    foreach (var step in steps)
                    {
                        token = new JObject();
                        token["distance"] = step["distance"]["text"];
                        token["instruction"] = step["html_instructions"];
                        token["polyline"] = step["polyline"]["points"];
                        jsonSteps.Add(token);
                    }
                    json["route"] = jsonSteps;
                    return json.ToString();
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
            }
            return DriverHandle.INTERNAL_ERROR;
        }

        public static string getRoute(double[] from, double[] to)
        {
            try
            {
                String link = "https://maps.googleapis.com/maps/api/directions/json?mode=driving&origin=";
                link += from[0].ToString().Replace(',', '.') + "," + from[1].ToString().Replace(',', '.') + "&destination=";
                link += to[0].ToString().Replace(',', '.') + "," + to[1].ToString().Replace(',', '.') + "&key=" + key;
                return readLink(link);
            }
            catch (Exception ex)
            {
                print(ex.ToString());
            } return DriverHandle.INTERNAL_ERROR;
        }

        public static string readLink(string url) {
            try {
                WebClient wc = new WebClient();
                print(url);
                return wc.DownloadString(url);
            } catch(Exception ex) {
                print(ex.ToString());
            } return DriverHandle.INTERNAL_ERROR;
        }

        public static double distance(double[] from, double[] to) {
            try {
                double lon1, lat1, lon2, lat2;
                lat1 = from[0];
                lon1 = from[1];
                lat2 = to[0];
                lon2 = to[1];
                var r = 6371.0;
                var phi1 = toRad(lat1);
                var phi2 = toRad(lat2);
                var deltaPhi = toRad(lat2 - lat1);
                var deltaLambda = toRad(lon2 - lon1);
                var a = Math.Pow(Math.Sin(deltaPhi / 2.0), 2) + Math.Cos(phi1) * Math.Cos(phi2) * Math.Pow(Math.Sin(deltaLambda / 2.0), 2);
                var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
                return r * c;
            } catch (Exception ex) {
                print(ex.ToString());
            }
            return 0;
		}

        public static double toRad(double angle) {
            return Math.PI * angle / 180;
        }

        public static double[] getCoords(string coords) {
            try {
                string[] parts = coords.Replace('.', ',').Split(':');
                return new double[] { Double.Parse(parts[0]), Double.Parse(parts[1]) };
            } catch (Exception ex) {
                print("invalid coords given " + coords + ex);
            }
            return null;
        }

        public static bool isNull(Object obj) { return obj == null; }

        public static void print(string text) { System.Diagnostics.Debug.WriteLine(text); }

        public static SqlDataReader query(string request)
        {
            try
            {
                print(request);
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conn"].ToString());
        conn.Open();
                SqlCommand command = new SqlCommand(request, conn);
                command.CommandTimeout = 0;
                return command.ExecuteReader();
            } 
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
                print("Failed: " + request);
            }
            return null;
        }

        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }
    }
}
