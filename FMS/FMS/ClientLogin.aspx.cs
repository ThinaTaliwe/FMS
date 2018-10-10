using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FMS
{
    public partial class ClientLogin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            SmtpClient client = new SmtpClient();
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.EnableSsl = true;
            client.Host = "smtp.gmail.com";
            client.Port = 587;

            // setup Smtp authentication
            System.Net.NetworkCredential credentials =
                new System.Net.NetworkCredential("khamorudu@gmail.com", "AndileAyanda123!");
            client.UseDefaultCredentials = false;
            client.Credentials = credentials;

            MailMessage msg = new MailMessage();
            msg.From = new MailAddress("khamorudu@gmail.com");
            msg.To.Add(new MailAddress("216090091@student.uj.ac.za"));

            msg.Subject = "Details for upcoming delivery";
            msg.IsBodyHtml = true;
            msg.Body = string.Format("<html><head></head><body><b>Hi There </b> </br> Thank you for using new era commerce, you can get more information about the delivery and confirm once it has been delivered on this <a href= " + " http://localhost:53436/Client_Deliv?order=1234 " + "> </a> </ body > ");

            try
            {
                client.Send(msg);
                Page.Response.Redirect(Page.Request.Url.ToString(), true);
            }
            catch (Exception ex)
            {
                Response.Write("Could not send the e-mail - error: " + ex.Message);
            }

        }
    }
}