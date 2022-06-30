using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vinasa.Models;

namespace Vinasa.Controllers
{
    public class AccountController : Controller
    {
        SEP25Team16Entities2 db = new SEP25Team16Entities2();
        private string loginEmail;
        private string loginPassword;
        // GET: Account
        [HttpGet]
        public ActionResult Login()
        {
            AdminAccountModels accountModels = new AdminAccountModels();
            return View(accountModels);
        }

        [HttpPost]
        public ActionResult Login(AdminAccountModels adminAccountModels)
        {
            loginEmail = adminAccountModels.Email;
            loginPassword = adminAccountModels.MatKhau;

            using (db)
            {
                var checkAccount = db.TAIKHOANADMINs.Where(acc => acc.Email.Equals(loginEmail.Trim()) && acc.MatKhau.Equals(loginPassword.Trim())).FirstOrDefault();
                {
                    if (checkAccount != null)
                    {
                        return RedirectToRoute("HomeLogged");
                    }
                }
            }

            return Content("Login fail" + loginEmail + " " + loginPassword);
        }

        public ActionResult CreateAccount()
        {
            return View();
        }
        public ActionResult Profile()
        {
            return View();
        }
    }

}