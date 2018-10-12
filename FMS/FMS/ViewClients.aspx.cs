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
            List<int> lstIDs = new List<int>();
            if (rows.HasRows)
            {
                //Order.InnerHtml = rows.GetString(0);
                while (rows.Read())
                    lstIDs.Add(rows.GetInt32(0));
                foreach (var id in lstIDs)
                {
                    Client client = new Client(id);
                    HTMLStr += "<tr> <td> " + client.getName() + "</td> <td> " + client.getCompany() + "</td> <td> " + client.getTelephone() + "</td> <td> " + client.getEmail() + "</td> <td> " + client.getLocation() + "</td> </tr>";
                }
                Util.print(HTMLStr);
                tables.InnerHtml = HTMLStr;
            }
        }
    }
}