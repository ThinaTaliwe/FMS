using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FMS.App_Code;

namespace FMS
{
    public partial class Home : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {

            var query = "select order_num, truck, driver, client, accepted from Delivery";
            var rows = Util.query(query);
            var HTMLStr = "";
            if (rows.HasRows)
            {
                var assigned = 0;//(rows.GetInt32(14));
                var assignedStr = "";
                while (rows.Read())
                {
                    assignedStr = rows.GetInt32(4) == 0 ? "No" : "Yes";
                    HTMLStr += "<tr> <td> " + Convert.ToString(rows.GetString(0)) + "</td> <td> " + Convert.ToString(rows.GetString(1)) + "</td> <td> " + new Driver(rows.GetString(2)).getName() + "</td> <td> " + new Client(rows.GetInt32(3)).getName() + "</td> <td> "  + assignedStr + "</td> </tr>";
                }
                tables.InnerHtml = HTMLStr;
            }
        }
    }
}