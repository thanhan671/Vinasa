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
        #region global variable

        SEP25Team16Entities2 db = new SEP25Team16Entities2();
        private string loginEmail;
        private string loginPassword;

        #endregion

        #region main method
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
                    else if (checkAccount != null && !checkAccount.TrangThai.Equals(1))
                    {
                        ViewBag.Message = checkAccount.Email + "  tai khoan nay chua duoc kich hoat, vui long lien he voi quan tri vien de kich hoat tai khoan";
                        return View();
                    }

                }
            }

            ViewBag.Message = "Tai khoan hoac mat khau khong dung";
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
        public new ActionResult Profile()
        {
            int id;
            if (Session["AccountID"] != null)
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
            int id = (int)Session["AccountID"];
            var name = adminAccountModels.Ten.Trim();
            var email = adminAccountModels.Email.Trim();
            var phoneNumber = adminAccountModels.Sdt.Trim();
            var oldPassword = adminAccountModels.MatKhau;
            var newPassword = adminAccountModels.newMatKhau;
            var rePassword = adminAccountModels.reMatKhau;

            if (!ModelState.IsValid)
            {
                if (oldPassword != null)
                {
                    using (db)
                    {
                        var accountCheck = db.TAIKHOANADMINs.Where(acc => acc.ID.Equals(id)).FirstOrDefault();
                        if (CheckPassword(oldPassword, accountCheck.MatKhau))
                        {
                            accountCheck.Ten = name;
                            accountCheck.Email = email;
                            accountCheck.Sdt = phoneNumber;
                            if (newPassword != null)
                            {
                                if (CheckPassword(newPassword, rePassword))
                                {
                                    accountCheck.MatKhau = newPassword;
                                }
                                else
                                {
                                    ViewBag.Message = "Mat khau moi va xac nhan mat khau khong trung khop!";
                                    return View();
                                }
                            }
                        }
                        else
                        {
                            TempData["Message"] = "Mật khẩu cũ không chính xác";
                            return View();
                        }
                        db.SaveChanges();
                    }
                }
                else
                {
                    ViewBag.Message = "Nhập mật khẩu để thay đổi thông tin";
                    return View();
                }
            }

            return RedirectToAction("Index", "Home", new { area = " " });
        }
        #endregion

        #region support method
        private bool CheckPassword(string pass1, string pass2)
        {
            if (!pass1.Trim().Equals(pass2.Trim()))
            {
                return false;
            }
            return true;
        }

        #endregion

    }
}