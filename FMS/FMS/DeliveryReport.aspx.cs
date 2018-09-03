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
            }
        }

        protected void viewReport(object sender, EventArgs e) {
            var id = Convert.ToInt32(delivery.SelectedValue);
            Delivery deliv = Delivery.getInstance(id);
            if(deliv != null) {
                var json = deliv.speedInfo();
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
                }
                else
                    text.Text = "something else happened";           
            }
        }
    }
}