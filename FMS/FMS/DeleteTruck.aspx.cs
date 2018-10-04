using FMS.App_Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FMS
{
    public partial class DeleteTruck : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var query = "select id from Trucks";
            var rows = Util.query(query);

            if (rows.HasRows)
            {
                while (rows.Read())
                {
                    delTruck.Items.Add(new ListItem(rows.GetString(0)));
                }  
            }
            
        }

        protected void RemoveTruck(object sender, EventArgs e)
        {


        }

    }
}