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
            var query = "select [from], [to], truck, driver, client, started, completed, id from Delivery order by id desc";
            var rows = Util.query(query);
            var HTMLStr = "";
            var num = 0; 
            if (rows.HasRows)
            {
                while (rows.Read())
                {
                    num = rows.GetInt32(7); 
                    String From = Delivery.getAddress(rows.GetString(0));
                    String To = Delivery.getAddress(rows.GetString(1));
                    string start, end;
                    if(!rows.IsDBNull(5))
                    {
                        start = Convert.ToString(rows.GetDateTime(5).TimeOfDay);
                        if (!rows.IsDBNull(6))
                        {
                            end = Convert.ToString(rows.GetDateTime(6).TimeOfDay);
                        }
                        else end = "Not Completed";
                    } else
                    {
                        start = "Not Started";
                        end = "Not Applicable";
                    }

                    HTMLStr += "<tr> <td> " + new Client(rows.GetInt32(4)).getCompany() + "</td> <td> " + Convert.ToString(rows.GetString(2)) + "</td> <td> " + From + "</td> <td> " + To + "</td> <td> " + start + "</td> <td> " + end + "</td> <td> "  + "</td> <td> <a href=" + "EditDelivery?order=" + num + "> Edit </a> </tr>";
                }
                tables.InnerHtml = HTMLStr;
            }
        }
    }
}