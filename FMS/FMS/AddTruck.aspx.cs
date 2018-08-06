using FMS.App_Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FMS
{
    public partial class AddTruck : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Add_Truck(object sender, EventArgs e)
        {
            //Query for Adding the truck
            var query = "INSERT INTO TRUCKS(ID, LOAD, SPEED) VALUES('" + TruckPlate.Value + "', '" + TruckMaxLoad.Value + "', '" + TruckMaxSpeed.Value  + "');";
            Util.query(query);
            Page.Response.Redirect(Page.Request.Url.ToString(), true);
        }
    }
}