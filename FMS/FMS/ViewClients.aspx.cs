using FMS.App_Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FMS
{
    public partial class ViewClients : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var query = "select company, name, telephone, email, location from Clients";
            var rows = Util.query(query);
            //var client = Util.getClient() 
            var HTMLStr = "";
            if (rows.HasRows)
            {
                while (rows.Read())
                {

                    HTMLStr += "<tr> <td> " + rows.GetString(0) + "</td> <td> " + rows.GetString(1)  + "</td> <td> " + rows.GetString(2) + "</td> <td> " + rows.GetString(3) + "</td> <td> " + rows.GetString(4) + "</td> </tr>";

                }
                tables.InnerHtml = HTMLStr;
            }
        }
    }
}