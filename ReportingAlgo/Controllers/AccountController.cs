using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ReportingAlgo.Models;

namespace ReportingAlgo.Controllers
{
    public class AccountController : Controller
    {
        string _username = "geovanem@msn.com";
        string _password = "Spiderman!23";

        

        [HttpGet]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel loginviewModel)
        {

            if (!ModelState.IsValid)
            {
                return View(loginviewModel);
            }

            if (_password.Equals(loginviewModel.Password) && _username.Equals(loginviewModel.Email))
            {
                if (Session["userId"] == null)
                {
                    Session["userId"] = loginviewModel.Email;
                }
            }
            string username = loginviewModel.Email;
            string password = loginviewModel.Password;

            return RedirectToAction("EditTransactionsPage", "Actual");
        }

        [HttpGet]
        public ActionResult Logout()
        {
            Session["userId"] = null;

            return RedirectToAction("Index", "Actual");
        }


    }
}