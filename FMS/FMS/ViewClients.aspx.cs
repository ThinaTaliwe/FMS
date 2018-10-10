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
            var query = "select id from Clients";
            var rows = Util.query(query);
            var HTMLStr = "";
            List<string> lstIDs = new List<string>();
            if (rows.HasRows)
            {
                Response.Write("WE HAVE ROWS");
                //Order.InnerHtml = rows.GetString(0);
                while (rows.Read())
                    lstIDs.Add(Convert.ToString(rows.GetInt32(0)));
                Response.Write("WE HAVE IDS");
                foreach (var id in lstIDs)
                {
                    Client client = new Client(id);
                    HTMLStr += "<tr> <td> <a href='ClientInfo?id=" + client.getID() + "></a></td> <td> " + client.getName() + "</td> <td> " + client.getCompany() + "</td> <td> " + client.getTelephone() + "</td> <td> " + client.getEmail() + "</td> <td> " + client.getLocation() + "</td> </tr>";
                }
                tables.InnerHtml = HTMLStr;
            }
        }
    }
}