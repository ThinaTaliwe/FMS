using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FMS.App_Code; 

namespace FMS
{
    public partial class ViewAllTrucks : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var query = "select id, load, speed from Trucks";
            var rows = Util.query(query);
            //var client = Util.getClient() 
            var HTMLStr = "";
            if (rows.HasRows)
            {
                while (rows.Read())
                {
                    HTMLStr += "<tr> <td> " + rows.GetString(0) + "</td> <td> " + rows.GetInt32(1) + "</td> <td> " + rows.GetInt32(2) + "</td> </tr>";
                }
                tables.InnerHtml = HTMLStr;
            }
        }
    }
}