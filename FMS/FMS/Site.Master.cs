﻿using System;
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
            if (Session["user"] == null)
            {
                User u = new Admin("1234567890123");
                Session["user"] = u;
                Session["name"] = u.getName();
                user.InnerText = u.getName();
            } else
            {
                User u = (User)Session["user"];
            }
        }
    }
}