using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FMS.App_Code;
using System.Security.Cryptography;

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

            String name = "1234567890123";//Username.Value;
            String pass = "MMELI"; //password.Value;
            //hash password here 
            var query = "select id, password from users where id like '" + name + "';";
            var response = Util.query(query);

            if (username.Value == string.Empty)
            {
                valdUser.Text = "Enter a username";
            }
            if (password.Value == string.Empty)
            {
                valdPass.Text = "Enter a password";
            }

            if(response.HasRows) {
                while(response.Read()) {
                    if (pass == response.GetString(1))
                    {
                        Session["user"] = response.GetString(0);
                        Response.Redirect("Home");
                    } else {
                        //login failed 
                    }
                }
            } else {
                Response.Write("Login Failed");
                System.Diagnostics.Debug.WriteLine("login failed");
            }
        }
    }
}