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
            var query = "select id, brand, load, speed, class_code from Trucks";
            var rows = Util.query(query);
            //var client = Util.getClient() 
            var HTMLStr = "";
            if (rows.HasRows)
            {
                //Order.InnerHtml = rows.GetString(0);
                while (rows.Read())
                {
                    HTMLStr += "<tr>  <td> <a href='TruckInfo.aspx'>" + rows.GetString(0) + "</a></td> <td> " + rows.GetString(1) + "</td> <td> " + rows.GetInt32(2) + "</td><td> " + rows.GetInt32(3) + "</td><td> " + rows.GetString(4) + "</td></tr>";
                }
                tables.InnerHtml = HTMLStr;
            }  
        }
    }
}