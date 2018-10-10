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
            string check = Request.QueryString["id"];
            viewDelivery(Convert.ToInt32(check));
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
                    var strInfo = "Distance: " + json["distance"] + "<br/>";
                    strInfo += "Average Speed: " + json["speed"] + "<br/>";
                    strInfo += "Time (in hours): " + json["time"] + "<br/>";
                    strInfo += "Overspeeding Ratio: " + json["overspeed_ratio"];
                    text.Text = strInfo;
                }
                else
                    text.Text = "Delivery doesn't have speed information";
            }
        }
    }
}