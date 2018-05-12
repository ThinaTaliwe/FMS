using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FMS.App_Code;

namespace FMS
{
    public partial class CreateDelivery : System.Web.UI.Page
    {
        private Delivery delivery = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string driversQuery = "SELECT NAME FROM USERS WHERE USER_TYPE LIKE 'DRIVER'";
                string truckQuery = "SELECT ID FROM TRUCKS";
                string clientQuery = "SELECT COMPANY FROM CLIENTS ";
                var drivers = Util.query(driversQuery);
                if (drivers.HasRows)
                {
                    while (drivers.Read())
                    {
                        DriverChosen.Items.Add(new ListItem(drivers.GetString(0)));
                    }
                }
                var trucks = Util.query(truckQuery);
                if (trucks.HasRows)
                {
                    while (trucks.Read())
                    {
                        TruckChosen.Items.Add(new ListItem(trucks.GetString(0)));
                    }
                }
                var clients = Util.query(clientQuery);
                if (clients.HasRows)
                {
                    while (clients.Read())
                    {
                        Client.Items.Add(new ListItem(clients.GetString(0)));
                    }
                }
            }
        }

        public void newDelivery(object obj, EventArgs e)
        {
            Error.InnerText = "Successfully Created Delivery";
            delivery = new Delivery();
            delivery.setOrderNum(OrderNum.Value);
            delivery.setClient(new Client(Client.Value));
            string[] date = DeliveryDate.Value.Split('/');
            DateTime delivDate = new DateTime();
            delivDate.AddYears(Convert.ToInt32(date[0]));
            delivDate.AddMonths(Convert.ToInt32(date[1]));
            delivDate.AddDays(Convert.ToInt32(date[2]));
            delivery.setMaterial(Material.Value);
            delivery.setLoad(Convert.ToInt32(Load.Value));
            delivery.setFrom(StartRoute.Value);
            delivery.setFrom(EndRoute.Value);
            Error.InnerText = "Delivery Created";
        }

        protected void btn_Click(object sender, EventArgs e)
        {
<<<<<<< HEAD
            Error.InnerText = "Successfully Created Delivery";
            delivery = new Delivery();
            delivery.setOrderNum(OrderNum.Value);
            delivery.setTruck(TruckChosen.Value);
            var query = "SELECT ID FROM USERS WHERE NAME LIKE '" + DriverChosen.Value + "';";
            var dr = Util.query(query);
            if(dr.Read())
            {
                delivery.setDriver(dr.GetString(0));
            }
            delivery.setClient(new Client(Client.Value));
            //string[] date = DeliveryDate.Value.Split('/');
            //DateTime delivDate = new DateTime();
            //delivDate.AddYears(Convert.ToInt32(date[0]));
            //delivDate.AddMonths(Convert.ToInt32(date[1]));
            //delivDate.AddDays(Convert.ToInt32(date[2]));
            delivery.setDepartDay(DateTime.Now);
            delivery.setMaterial(Material.Value);
            delivery.setLoad(Convert.ToInt32(Load.Value));
            delivery.setFrom(StartRoute.Value);
            delivery.setTo(EndRoute.Value);
            delivery.save((Session["user"] as User).getID());
            
            //Pop Up With Message and Refresh Screen 
            
            //Response.Redirect("CreateDelivery");
            //string message = "Delivery Created";
            string message = "Order Placed Successfully.";
            string script = "window.onload = function(){ alert('";
            script += message;
            script += "')};";
            ClientScript.RegisterStartupScript(this.GetType(), "SuccessMessage", script, true);

            //ClientScript.RegisterStartupScript(GetType(), "alert", "alert('" + message + "');", true);
=======
            var query = "INSERT INTO DELIVERY(ORDER_NUM, TRUCK, DRIVER, CLIENT, [FROM], [TO], MATERIAL, [LOAD], DEPART_DAY, AUTHORITY) VALUES('" + OrderNum.Value + "', '" + TruckChosen.Value + "', '" + DriverChosen.Value + "', '" + Client.Value + "', '" + StartRoute.Value + "', '" + EndRoute.Value + "', '" + Material.Value + "', '" + Load.Value + "', '" + DeliveryDate.Value + "', '" + "1234567890123" + "');";
            Delivery delivery = Delivery.getInstance(OrderNum.Value);
            System.Diagnostics.Debug.WriteLine(query + delivery.getDriver().getID() + delivery);
            Error.InnerText = "Delivery Created";
>>>>>>> ffdde4ac6847d968b8f8b8c364a93284ff83cc67
        }
    }
}