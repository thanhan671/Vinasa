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
                    if (checkAccount != null && checkAccount.TrangThai.Equals(1))
                    {
                        Session["AccountID"] = checkAccount.ID;
                        Session["UserName"] = checkAccount.Ten;
                        Session["AccountType"] = checkAccount.Quyen;
                        return RedirectToAction("Index", "Home", new { area = "" });
                    }
                    else if(checkAccount != null && !checkAccount.TrangThai.Equals(1))
                    {
                        ViewBag.Message = checkAccount.Email + " this account not activated, please contact the administrator to active this";
                        return View();
                    }

                }
            }
            ViewBag.Message = "wrong email or password";
            return View();
        }

        [HttpGet]
        public ActionResult Logout()
        {
            Session["AccountID"] = null;
            Session.Abandon();
            return RedirectToAction("Login", "Account", new { area = " " });
        }

        [HttpGet]
        public ActionResult Register()
        {
            AdminAccountModels accountModels = new AdminAccountModels();
            return View(accountModels);
        }

        [HttpPost]
        public ActionResult Register(AdminAccountModels adminAccountModels)
        {
            var registerEmail = adminAccountModels.Email.Trim();
            var registerPassword = adminAccountModels.MatKhau.Trim();
            var registerRePassword = adminAccountModels.reMatKhau.Trim();

            if (registerPassword.Equals(registerRePassword))
            {
                using (db)
                {
                    var checkAccount = db.TAIKHOANADMINs.Where(acc => acc.Email.Equals(registerEmail.Trim())).FirstOrDefault();
                    {
                        if (checkAccount != null)
                        {
                            ViewBag.Message = registerEmail + " has been existing, please change another email or contact to the admin";
                            return View();
                        }
                        else
                        {
                            try
                            {
                                TAIKHOANADMIN newAccount = new TAIKHOANADMIN();
                                newAccount.Ten = adminAccountModels.Ten.Trim();
                                newAccount.Email = registerEmail;
                                newAccount.Sdt = adminAccountModels.Sdt.Trim();
                                newAccount.PhongBan = adminAccountModels.PhongBan.Trim();
                                newAccount.MatKhau = registerPassword;
                                newAccount.Quyen = 2;
                                newAccount.TrangThai = 2;

                                db.TAIKHOANADMINs.Add(newAccount);
                                db.SaveChanges();
                                ViewBag.Message = newAccount.Ten + " create successfully, please wait for admin to activate this account";
                                //return RedirectToAction("Login");
                                return View();
                            }
                            catch (Exception ex)
                            {
                                throw ex;
                            }
                        }
                    }
                }
            }
            return View();
        }

        [HttpGet]
        public new ActionResult Profile()
        {
            int id; 
            if(Session["AccountID"] != null)
            {
                id = (int)Session["AccountID"];
            }
            else
            {
                return RedirectToAction("Login", "Account", new { area = " " });
            }

            AdminAccountModels adminAccountModels = db.TAIKHOANADMINs.Where(acc => acc.ID.Equals(id)).Select(acc => new AdminAccountModels()
            {
                Ten = acc.Ten,
                Email = acc.Email,
                Sdt = acc.Sdt,
                MatKhau = acc.MatKhau
            }).SingleOrDefault();

            return View(adminAccountModels);
        }

        [HttpPost]
        public new ActionResult Profile(AdminAccountModels adminAccountModels)
        {

            return RedirectToAction("Index", "Home", new { area = " " });
        }
    }
}