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
                string driversQuery = "SELECT NAME, ID FROM USERS WHERE USER_TYPE LIKE 'DRIVER'";
                string truckQuery = "SELECT ID FROM TRUCKS";
                string clientQuery = "SELECT COMPANY, ID FROM CLIENTS ";
                var drivers = Util.query(driversQuery);
                if (drivers.HasRows)
                {
                    while (drivers.Read())
                    {
                        DriverChosen.Items.Add(new ListItem(drivers.GetString(0) + " " + drivers.GetString(1)));
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
                Client.Items.Add(new ListItem("Other")); 
            }
        }

        protected void btn_Click(object sender, EventArgs e)
        {
            string tempIDquery = "SELECT ID FROM CLIENTS WHERE COMPANY LIKE '" + Client.Value + "'";
            var tempID = Util.query(tempIDquery);
            int IDnow = 0; 
            if (tempID.HasRows)
            {
                tempID.Read();
                IDnow = tempID.GetInt32(0);
            }
            string driver = DriverChosen.Value.Split(' ')[1];
            var query = "INSERT INTO DELIVERY(ORDER_NUM, TRUCK, DRIVER, CLIENT, [FROM], [TO], MATERIAL, [LOAD], DEPART_DAY, AUTHORITY) VALUES('" + OrderNum.Value + "', '" + TruckChosen.Value + "', '" + driver + "', '" + IDnow + "', '" + "Location1" + "', '" + "Location2" + "', '" + Material.Value + "', '" + Load.Value + "', '" + DeliveryDate.Value + "', '" + "1234567890123" + "');";
            Util.query(query);
            Page.Response.Redirect(Page.Request.Url.ToString(), true);
        }
    }
}