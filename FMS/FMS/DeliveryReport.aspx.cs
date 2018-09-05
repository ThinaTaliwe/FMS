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
    public partial class DeliveryReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack) {
                string query = "select id from delivery";
                var delivs = Util.query(query);
                if (delivs.HasRows)
                {
                    while (delivs.Read())
                    {
                        delivery.Items.Add(new ListItem(Convert.ToString(delivs.GetInt32(0))));
                    }
                }
                try
                {
                    string id = Page.RouteData.Values["id"].ToString();
                    viewDelivery(Convert.ToInt32(id));
                }
                catch (Exception ex)
                {
                    Util.print(ex.ToString());
                }
            }
        }

        protected void viewReport(object sender, EventArgs e) {
            var id = Convert.ToInt32(delivery.SelectedValue);
            viewDelivery(id);
        }

        private void viewDelivery(int id) {
            Delivery delivery = Delivery.getInstance(id);
            if(delivery != null) {
                var json = delivery.speedInfo();
                if (json != null)
                {
                    JArray info = (JArray)json["info"];
                    string location;
                    foreach (JObject token in info)
                    {
                        location = token["distance"] + "*";
                        location += token["speed"] + "*";
                        location += token["time"] + "*";
                        location += token["coords"];
                        locations.Value += location + "#";
                        location = "";
                    }
                    text.Text += "Distance: " + json["distance"] + "\n";
                    text.Text += "Average Speed: " + json["speed"] + "\n";
                    text.Text += "Time (in hours): " + json["time"] + "\n";
                    text.Text += "Overspeeding Ratio: " + json["overspeed_ratio"];
                }
                else
                    text.Text = "something else happened";
            }
        }
    }
}