using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//
using System.Security.Cryptography;

namespace FMS.App_Code
{
    /// <summary>
    ///  SHA1 is more secure than MD5
    /// </summary>
    public class Secrecy{
        public string HashPassword(String StrPassword)
        {
            SHA1 HashAlgorithm = SHA1.Create();
            //Byte array to store the returned hashed data
            byte[] hashedData;
            //Convert the input string to a byte array and compute the hash.
            hashedData = HashAlgorithm.ComputeHash(Encoding.Default.GetBytes(StrPassword));
            //String variable that will store the returned hashed string
            string hashedPassword = "";

            //Loop through each byte of the hashed data and format each one as a hexadecimal string.
            for (int i = 0; i < hashedData.Length - 1; i++)
            {
                hashedPassword += hashedData[i].ToString("x2");
            }

            //Return the hexadecimal string.
            return hashedPassword;
        }
    } }
