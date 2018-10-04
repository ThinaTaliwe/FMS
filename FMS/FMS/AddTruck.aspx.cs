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
            Color.Items.Add(new ListItem("Black"));
            Color.Items.Add(new ListItem("White"));
            Color.Items.Add(new ListItem("Blue"));
            Color.Items.Add(new ListItem("Grey"));
            Color.Items.Add(new ListItem("Silver"));

        }

        protected void Add_Truck(object sender, EventArgs e)
        {
            //Query for Adding the truck
            var query = "INSERT INTO TRUCKS(ID, BRAND, LOAD, SPEED, CLASS_CODE) VALUES('" + TruckPlate.Value + "', '" + TruckBrand.Value + "', '" + TruckMaxLoad.Value + "', '" + TruckMaxSpeed.Value  + "', '" + TruckCode.Value + "');";
            Util.query(query);
            Page.Response.Redirect(Page.Request.Url.ToString(), true);
        }
    }
}