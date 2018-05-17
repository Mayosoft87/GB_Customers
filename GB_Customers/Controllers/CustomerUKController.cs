using GB_Customers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GB_Customers.Controllers
{
    public class CustomerUKController : Controller
    {


        /// <summary>
        /// Inicializaction of the View
        /// </summary>
        /// <param name="id"></param>
        /// <returns>View  with the CustomerModel </returns>
        [HttpGet]
        public ActionResult AddOrEditUK(int id = 0)
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
        public ActionResult ConfirmUK(String email)
        {
            try
            {
                Customer customerModel = new Customer();
                Encryption objDeco = new Encryption();
                using (CustomerConsentEntities dbModel = new CustomerConsentEntities())
                {
                    customerModel.email = objDeco.EmailDecrypt(email);
                    customerModel.active = true;
                    customerModel.created = DateTime.Now;
                    customerModel.lastUpdate = DateTime.Now;
                    customerModel.distributionGroup = "Windymains Stock Grid";
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
            return View("ConfirmUK");
        }

        /// <summary>
        /// Add or Edit the subscriptions
        /// </summary>
        /// <param name="customerModel">Customer Obj</param>
        /// <returns>View</returns>
        [HttpPost]
        public ActionResult AddOrEditUK(Customer customerModel)
        {
            try
            {
                EmailClient objEmail = new EmailClient();
                Encryption objDeco = new Encryption();
                String email = objDeco.EmailEncryption(customerModel.email);
                String htmlEmail = objEmail.GetMessageHTML(email,"UK");
                objEmail.SendEmail(htmlEmail, customerModel.email);
                ModelState.Clear();
                ViewBag.SuccessMessage = "You will receive a Confirmation Email.";

            }
            catch (Exception e)
            {
                ViewBag.ErrorMessage = e.Message;
            }
            return View("AddOrEditUK", new Customer());
        }


    }
}
