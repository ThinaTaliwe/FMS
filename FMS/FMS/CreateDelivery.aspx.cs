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
        protected void Page_Load(object sender, EventArgs e)
        {
            string driversQuery = "SELECT NAME FROM USERS WHERE USER_TYPE LIKE 'DRIVER'";
            string truckQuery = "SELECT ID FROM TRUCKS";
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
        }
    }
}