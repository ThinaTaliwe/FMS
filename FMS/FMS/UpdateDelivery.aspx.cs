using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FMS.App_Code;


namespace FMS
{
    public partial class UpdateDelivery : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var query = "select * from Delivery";
            var rows = Util.query(query);
            var HTMLStr = "";
            if (rows.HasRows)
            {
                while (rows.Read())
                {

                    HTMLStr += "<tr> <td> " + Convert.ToString(rows.GetString(1)) + "</td> <td> " + Convert.ToString(rows.GetString(2)) + "</td> <td> " + Convert.ToString(rows.GetString(5)) + "</td> <td> " + Convert.ToString(rows.GetString(6)) + "</td> <td> <a href=" + "EditDelivery?order=" + "1" + "> Edit </a> </tr>";
                }
                tables.InnerHtml = HTMLStr;
            }
        }
    }
}