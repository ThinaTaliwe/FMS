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
            Login_Error.InnerText = "Logging in";
            var name = username.Value;
            var pass = password.Value;
            var query = "SELECT PASSWORD, ID FROM USERS WHERE NAME LIKE '" + name + "';";
            var users = Util.query(query);
            if(users.HasRows)
            {
                while (users.Read())
                {
                    if(pass == users.GetString(0))
                    {
                        Session["user"] = new User(Convert.ToString(users.GetString(1)));
                        Response.Redirect("Home");
                    }
                }
            } 
        }
    }
}