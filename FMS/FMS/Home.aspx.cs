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

            var query = "select [TO], [FROM], truck, driver, client, accepted from Delivery WHERE COMPLETED is null AND (Month(DEPART_DAY) = Month(getdate()) AND YEAR(DEPART_DAY) = YEAR(getdate()))";
            var rows = Util.query(query);
            //var client = Util.getClient() 
            var HTMLStr = "";
            if (rows.HasRows)
            {
                var assignedStr = "";
                while (rows.Read())
                {
                    try {
                        var date = rows.GetDateTime(5);
                        if (date != null) assignedStr = "Yes";
                        else assignedStr = "No";
                    } catch(Exception ex) {
                        Util.print(ex.ToString());
                        assignedStr = "No";
                    }
                    var Driver_MmeliThing = new Driver(rows.GetString(3));
                    HTMLStr += "<tr> <td> " + new Client(rows.GetInt32(4)).getCompany() + "</td> <td> " + Convert.ToString(rows.GetString(0)) + "</td> <td> " + Convert.ToString(rows.GetString(1)) + "</td> <td> " + Driver_MmeliThing.getName() + " " + Driver_MmeliThing.getSurname() + "</td> <td> " + "--" + "</td> <td> "  + assignedStr + "</td> </tr>";
                }
                tables.InnerHtml = HTMLStr;
            }
            
        }
    }
}