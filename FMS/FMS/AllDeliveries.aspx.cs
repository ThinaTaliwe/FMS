using FMS.App_Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FMS
{
    public partial class AllDeliveries : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var query = "select [from], [to], truck, driver, client, started, completed, id  from Delivery order by id desc";
            var rows = Util.query(query);
            var HTMLStr = "";
            var num = 0;
            int orderNumID = 0;

            // Query a list of IDs
            var IDquery = "select id from Delivery";
            var IDrows = Util.query(IDquery);
            //Create List of string to store all these IDs
            List<String> lstIDz = new List<string>();
            List<Delivery> objLst = new List<Delivery>();
            int i = 0;

            // Check if IDrows has elements/data
            if (rows.HasRows || IDrows.HasRows)
            {
                while (rows.Read())
                {
                    while(IDrows.Read())
                    // Add all IDz to the list
                    lstIDz.Add(Convert.ToString(IDrows.GetInt32(0)));
                    //create object for all the IDZ
                    foreach (var id in lstIDz)
                    {
                        Delivery objDelivery = Delivery.getInstance(Convert.ToInt32(id));
                        objLst.Add(objDelivery);
                    }
                    num++;
                    orderNumID = rows.GetInt32(7);
                    String From = Delivery.getAddress(rows.GetString(0));
                    String To = Delivery.getAddress(rows.GetString(1));
                    string start, end;

                    if (!rows.IsDBNull(5))
                    {
                        start = Convert.ToString(rows.GetDateTime(5).TimeOfDay);
                        if (!rows.IsDBNull(6))
                        {
                            end = Convert.ToString(rows.GetDateTime(6).TimeOfDay);
                        }
                        else end = "Not Completed";
                    }
                    else
                    {
                        end = "Not Applicable";
                        start = "Not Applicable";
                    }
                        HTMLStr += "<tr> <td><a href='DeliveryInfo?id=" + objLst[i].getID() + "'>" + objLst[i].getID() + "</a></td><td> " + new Client(rows.GetInt32(4)).getCompany() + "</td> <td> " + Convert.ToString(rows.GetString(2)) + "</td> <td> " + From + "</td> <td> " + To + "</td> <td> " + start + "</td> <td> " + end + "</td> <td> " + "</td> <td> <a href=" + "DeliveryReport?id=" + orderNumID + "> Report </a> </td></tr>";
                  
                }
                tables.InnerHtml = HTMLStr;
            }

        }
    }
}