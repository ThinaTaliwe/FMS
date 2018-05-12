using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FMS.App_Code
{
    public class Driver : User
    {
        private string code;
        private DateTime firstIssue;
        private DateTime expiry;
        private int restriction;

        public Driver(string id) : base(id)
        {
            var query = "SELECT * FROM DRIVERS WHERE ID LIKE '" + id + "'";
            var driver = Util.query(query);
            if (driver.Read())
            {
                code = Convert.ToString(driver.GetString(1));
                firstIssue = driver.GetDateTime(2);
                expiry = driver.GetDateTime(3);
                restriction = Convert.ToInt32(driver.GetInt32(4));
            }
        }

        public void setCode(string value) { code = value; }
        public void setExpiry(DateTime value) { expiry = value; }
        public void setRestriction(int value) { restriction = value; }

        public string getCode() { return code; }
        public DateTime getFirstIssue() { return firstIssue; }
        public DateTime getExpiry() { return expiry; }
        public int getRetriction() { return restriction; }
    }
}