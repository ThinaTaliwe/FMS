using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FMS.App_Code;

namespace FMS
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            /*   if (Session["user"] == null)
               {
                   Response.Redirect("Login");
               } else
               {
                   string name = Session["user"] as string;
                   user.InnerText = new User(name).getName();
               } */

            if (Session["user"] == null)
                Session["user"] = "1234567890123";
        }
    }
}