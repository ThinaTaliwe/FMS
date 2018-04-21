using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FMS.App_Code
{
    public class User
    {
        private string name;
        private string ID;

        public User()
        {

        }

        public bool Login(String username, String password)
        {

            return false;
        }
        public bool logout()
        {
            return false;
        }

        public string hash(String password)
        {
            string strHash = "";

            return strHash;
        }
    }
}