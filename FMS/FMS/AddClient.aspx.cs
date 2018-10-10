using FMS.App_Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FMS
{
    public partial class AddClient : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Add_Client(object sender, EventArgs e)
        {
            var query = "INSERT INTO CLIENTS(NAME, COMPANY, TELEPHONE, EMAIL, LOCATION) VALUES('" + ClientName.Value + "', '" + ClientCompany.Value + "', '" + ClientTel.Value + "', '" + ClientEmail.Value  + "', '" + ClientAddress.Value + "');";
            Util.query(query);
            Page.Response.Redirect(Page.Request.Url.ToString(), true);
        }
    }
}