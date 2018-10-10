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
                }
              }
                tables.InnerHtml = HTMLStr;

            
         }

        protected void confirm_delivery(object sender, EventArgs e)
        {
            //Add to database
            string order = Request.QueryString["order"];
            var query = "SELECT id FROM DELIVERY WHERE order_num LIKE '" + order + "'";
            System.Diagnostics.Debug.WriteLine(query);
            var rows = Util.query(query);

            if (rows.HasRows)
            {
                while (rows.Read())
                {
                    Delivery delClient = Delivery.getInstance(rows.GetInt32(0));
                    var query2 = "UPDATE DELIVERY SET CONFIRMATION = '0' WHERE ID LIKE " + delClient.getID();
                    System.Diagnostics.Debug.WriteLine(query);
                    Util.query(query);
                    Page.Response.Redirect("Client_Thanks");

                }
            }
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
