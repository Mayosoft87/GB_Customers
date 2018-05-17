using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace GB_Customers.Models
{
    public class EmailClient
    {
        /// <summary>
        /// Create the client and send the email
        /// </summary>
        /// <param name="email">String</param>
        /// <param name="user">String</param>
        public void SendEmail(String email, String user)
        {

            SmtpClient client = new SmtpClient(); 
            try
            {
                client.Port = 25;
                client.Host = "mail.blacknight.com";
                client.EnableSsl = false;
                client.Timeout = 1000;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new System.Net.NetworkCredential("GlennonbrothersGDPR@PrivacyData.glennonbrothersit.ie", "n3qpoPhZqLJz");  
                MailMessage msjObj = new MailMessage();
                msjObj.To.Add(new MailAddress(user, user));
                msjObj.From = new MailAddress("GlennonbrothersGDPR@PrivacyData.glennonbrothersit.ie", "GlennonbrothersGDPR");

                if (email.Contains("Windymains"))
                {
                    msjObj.Subject = "GDPR- Confirm your interest in Windymains Stock Grid";
                }
                else
                {
                    msjObj.Subject = "GDPR- Confirm your interest in Glennon Brothers Stock List";
                }
                
                msjObj.IsBodyHtml = true;
                msjObj.Body = email;
                client.Send(msjObj);


            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                client = null;
            }

        }
        /// <summary>
        /// Created the HTML with the link
        /// </summary>
        /// <param name="Email">String</param>
        /// <returns>String</returns>
        public String GetMessageHTML(String Email,String country)
        {
            String link = string.Empty;
            String report = string.Empty; 
            if (country == "UK")
            {
                link = @"http://privacydata.glennonbrothersit.ie/CustomerUK/ConfirmUK?email=";
                report = "Windymains Stock Grid";
            }
            else
            {
                link= @"http://privacydata.glennonbrothersit.ie/Customer/Confirm?email=";
                report = "Glennon Brothers Stock List";
            }

            string newEmail;
            newEmail = Email + "\">Verify</a></H3>";
            return "<!DOCTYPE html> " +
                "<html xmlns=\"http://www.w3.org/1999/xhtml\">" +
                "<head>" +
                "</head>" +
                "<body style=\"font-family:'Century Gothic'\">" +
                "<img src =\"http://www.glennonbrothers.ie/images/logo.png\" style = \"width:300px;height:50px\" />" +
                "<H2>VERIFY YOUR EMAIL</H2>" +
                "<p>To complete the process, please click verify below: <H3> " + "<a href=\""+link + newEmail + "<br/><br/>If you did not subscribe to "+report+", simply ignore this email.</p>" +
                "</body>" +
                "</html>";
        }
    }
}