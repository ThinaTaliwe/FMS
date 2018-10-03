using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FMS.App_Code;

namespace FMS
{
    public partial class ViewDrivers : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var query = "select name, surname, email, ID from Users Where USER_TYPE like 'DRIVER'";
            var rows = Util.query(query);
            //var client = Util.getClient() 
            var HTMLStr = "";
            if (rows.HasRows)
            {
                while (rows.Read())
                {
                    var query2 = "select Code, Restriction, Expiry from DRIVERS WHERE ID like '" + rows.GetString(3) + "'";
                    var rows2 = Util.query(query2); 
                    if (rows2.HasRows)
                    {
                        while (rows2.Read())
                        {
                            HTMLStr += "<tr> <td> " + rows.GetString(0) + "</td> <td> " + "000 000 0000" + "</td> <td> " + rows.GetString(1) + "</td> <td> " + rows.GetString(2) + "</td> <td> " + rows2.GetString(0) + "</td> <td> " + rows2.GetDateTime(2) + "</td> <td> " + rows2.GetInt32(1) + "</td> </tr>";
                        }
                    }
                }
                tables.InnerHtml = HTMLStr;
            }
        }
    }
}