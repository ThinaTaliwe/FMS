using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FMS.App_Code;
using Newtonsoft.Json.Linq;

namespace FMS
{
    public partial class deliv : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void timer_Tick(object sender, EventArgs e)
        {
            var now = DateTime.Now.ToString();
            Util.print(now);
            Response.Write(now);
            update_text.Text = now;
        }
    }
}