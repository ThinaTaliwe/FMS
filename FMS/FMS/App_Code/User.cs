using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace FMS.App_Code
{
    /// <summary>
    /// Description of User.
    /// </summary>
        public abstract class User
        {

            // attributes
            private string name;
            private string ID;


            // methods
            public bool logIn(string userName, string password)
            {
                if (userName == this.name)
                {
                    // But we will use hashkey from database[NOT "this.hashKey(ID)"]
                    return hashKey(password) == this.hashKey(ID);
                }
                return false;
            }
            public bool logOut()
            {
            return false;
            }
            public string hashKey(string password)
            {
                Secrecy hashedPassword = new Secrecy();
                return hashedPassword.HashPassword(password);
            }
        }
}