using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;
using GB_Customers.Models;


namespace GB_Customers.Controllers
{
    public class CustomerController : Controller
    {

        /// <summary>
        /// Inicializaction of the View
        /// </summary>
        /// <param name="id"></param>
        /// <returns>View  with the CustomerModel </returns>
        [HttpGet]
        public ActionResult AddOrEdit(int id=0)
        {
            Customer customerModel = new Customer(); 
            return View(customerModel);
        }
        /// <summary>
        /// Allow anonymous to send the data
        /// </summary>
        /// <param name="email">String</param>
        /// <returns>View</returns>
        [AllowAnonymous]
        public ActionResult Confirm(String email)
        {
            try
            {
                Customer customerModel = new Customer();
                Encryption objDeco = new Encryption();
                using (CustomerConsentEntities dbModel = new CustomerConsentEntities())
                {
                    dbModel.Database.Connection.Open();
                    customerModel.email = objDeco.EmailDecrypt(email);
                    customerModel.active = true;
                    customerModel.created = DateTime.Now;
                    customerModel.lastUpdate = DateTime.Now;
                    customerModel.distributionGroup = "Glennon Brothers Stock List";
                    if (!dbModel.Customers.Any(c => c.email == customerModel.email && c.distributionGroup == customerModel.distributionGroup))
                    {
                        dbModel.Customers.Add(customerModel);
                        dbModel.SaveChanges();
                    }
              
                }
                ViewBag.ErrorMessage = "";
            }
            catch (Exception e)
            {
                ViewBag.ErrorMessage = e.Message;
            }
            return View("Confirm");
        }

        /// <summary>
        /// Add or Edit the subscriptions
        /// </summary>
        /// <param name="customerModel">Customer Obj</param>
        /// <returns>View</returns>
        [HttpPost]
        public ActionResult AddOrEdit(Customer customerModel)
        {
            try
            {
                EmailClient objEmail = new EmailClient();
                Encryption objDeco = new Encryption();
                String email = objDeco.EmailEncryption(customerModel.email);
                String htmlEmail = objEmail.GetMessageHTML(email,"IE");
                objEmail.SendEmail(htmlEmail, customerModel.email);
                ModelState.Clear();
                ViewBag.SuccessMessage = "You will receive a Confirmation Email";
                
            }
            catch (Exception e)
            {
                ViewBag.ErrorMessage = e.Message;
            }
            return View("AddOrEdit", new Customer());
        }

   
    }
}









  