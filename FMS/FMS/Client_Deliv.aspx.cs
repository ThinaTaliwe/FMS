using FMS.App_Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json.Linq;

namespace FMS
{
    public partial class Client_Deliv : System.Web.UI.Page
    {
        private Delivery delivery;

        protected void Page_Load(object sender, EventArgs e)
        {
            string order = Request.QueryString["order"];
            //ordernum.Text = id;
            //Display Info regarding the asked order 
            var query = "SELECT id FROM DELIVERY WHERE order_num LIKE '" + order + "'";
            System.Diagnostics.Debug.WriteLine(query);
            var rows = Util.query(query);

            //ordernum.Text = "Bye"; 
            var HTMLStr = "";
            if (rows.HasRows)
            {
                while (rows.Read())
                {
                    Delivery delClient = Delivery.getInstance(rows.GetInt32(0));
                    HTMLStr += "<h3> Delivery Information </h3> " + "<p> Order Number: " + delClient.getOrderNumber() + "</p> <p> Truck Plate: " + delClient.getTruck().getID() + "</p> </p> Driver Name: " + delClient.getDriver().getName() + "</p> <p> Origin: " + delClient.getFromAddress() + "</p> <p> Destination: " + delClient.getToAddress() + "</p> <p> Load (tons): " + delClient.getLoad() + "</p> <p> ETA: " + delClient.ETA() + " hours </p> <p> Material: " + delClient.getMaterial()  + "</p>";
                    viewDelivery(delClient.getID());
                    delivery = delClient;
                }
              }
                tables.InnerHtml = HTMLStr;

            
         }

        protected void confirm_delivery(object sender, EventArgs e)
        {
            //Add to database
            DateTime now = DateTime.Now;
            var query = "UPDATE DELIVERY SET CONFIRMATION = '" + now + "' WHERE ID LIKE " + delivery.getID();
            Util.query(query);
            Page.Response.Redirect("Client_Thanks");
        }

        private void viewDelivery(int id)
        {
            Delivery delivery = Delivery.getInstance(id);
            if (delivery != null)
            {
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
                }
                else
                {

                }
            }
        }

    }
}
