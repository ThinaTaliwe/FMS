using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FMS.App_Code;
using Newtonsoft.Json.Linq;

namespace FMS
{
    public partial class deliv : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string query;
            if (!IsPostBack) {
                query = "select id from delivery";
                var delivs = Util.query(query);
                if (delivs.HasRows)
                {
                    while (delivs.Read())
                    {
                        delivery.Items.Add(new ListItem(Convert.ToString(delivs.GetInt32(0))));
                    }
                }
                query = "select id from users where user_type like 'driver'";
                var dr = Util.query(query);
                if(dr.HasRows) {
                    while (dr.Read())
                        driver.Items.Add(new ListItem(dr.GetString(0)));
                }
                query = "select id from trucks";
                var tr = Util.query(query);
                if(tr.HasRows) {
                    while (tr.Read())
                        truck.Items.Add(new ListItem(tr.GetString(0)));
                }

            }
            //query = "select location from locations where delivery like 12";
            //var locs = Util.query(query);
            //if(locs.HasRows) {
            //    string strLocs = "";
            //    while (locs.Read())
            //        strLocs += locs.GetString(0) + "*";
            //    locations.Value = strLocs;
            //}
        }

        protected void getDeliv(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(delivery.SelectedValue);
            string tr, dr;
            tr = truck.SelectedValue;
            dr = driver.SelectedValue;
            Delivery d = Delivery.getInstance(id);
            Truck t = new Truck(tr);
            Driver dri = new Driver(dr);
            string query = "select location from locations where delivery like '" + delivery.SelectedValue + "' order by time asc";
            var reader = Util.query(query);
            List<string> list = new List<string>();
            if(reader.HasRows) {
                while (reader.Read())
                    list.Add(reader.GetString(0));
            }

            text.Text = t.getID() + "  " + dri.getName() + Util.totalDistance(list);
            if(d.speedInfo() != null) {
                var json = d.speedInfo();
                text.Text += json.ToString();
                JArray info = (JArray) json["info"];
                string location;
                foreach (JObject token in info) {
                    location = token["distance"] + "*";
                    location += token["speed"] + "*";
                    location += token["time"] + "*";
                    location += token["coords"];
                    locations.Value += location + "#";
                    location = "";
                }
            }
        }
    }
}