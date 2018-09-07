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
            var query = "select name, surname, email from Users Where USER_TYPE like 'DRIVER'";
            var rows = Util.query(query);
            //var client = Util.getClient() 
            var HTMLStr = "";
            if (rows.HasRows)
            {
                while (rows.Read())
                {
                    HTMLStr += "<tr> <td> " + rows.GetString(0) + "</td> <td> " + rows.GetString(1) + "</td> <td> " + rows.GetString(2) + "</td> </tr>";
                }
                tables.InnerHtml = HTMLStr;
            }
        }
    }
}