using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GB_Customers.Models
{
    public class Encryption
    {

        /// <summary>
        /// Encrypt the Email
        /// </summary>
        /// <param name="email">String</param>
        /// <returns>String</returns>
        public String EmailEncryption(String email)
        {
            try
            {
                String decoEmail = String.Empty;
                byte[] encryted = System.Text.Encoding.Unicode.GetBytes(email);
                decoEmail = Convert.ToBase64String(encryted);
                return decoEmail;
                
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        /// Decrypt the Email
        /// </summary>
        /// <param name="decoEmail">String</param>
        /// <returns>String</returns>
        public String EmailDecrypt(String decoEmail)
        {
            try
            {
                String textEmail = String.Empty;
                byte[] decrypted = Convert.FromBase64String(decoEmail);
                textEmail = System.Text.Encoding.Unicode.GetString(decrypted);
                return textEmail;

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}