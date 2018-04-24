using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FMS.App_Code;

namespace FMS
{
    public partial class Login : System.Web.UI.Page
    {
        private User user = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] != null)
                Response.Redirect("Home");
        }

        protected void logon(object sender, EventArgs e)
        {

        }
    }
}