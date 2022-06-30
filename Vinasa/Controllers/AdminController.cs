using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vinasa.Models;
using System.Threading.Tasks;

namespace Vinasa.Controllers
{
    public class AdminController : Controller
    {
        #region global variable
        SEP25Team16Entities2 db = new SEP25Team16Entities2();
        int currentRole;
        #endregion

        // GET: Admin
        [HttpGet]
        public ActionResult Index()
        {
            if (Session["AccountID"] == null)
            {
                return RedirectToAction("Login", "Account", new { area = " " });
            }

            var data = db.TAIKHOANADMINs.ToList();
            ViewBag.adminDetails = data;
            return View();
        }

        [HttpPost]
        public ActionResult Index(int accountID)
        {
            if (Session["AccountID"] == null)
            {
                return RedirectToAction("Login", "Account", new { area = " " });
            }

            return View();
        }

        [HttpGet]
        public ActionResult CreateAccount()
        {
            AdminAccountModels accountModels = new AdminAccountModels();
            accountModels.RoleList = new SelectList(db.QUYENs, "IDQuyen", "TenQuyen", accountModels.Quyen);
            accountModels.StatusList = new SelectList(db.TRANGTHAIs, "IDTrangThai", "TenTrangThai", accountModels.TrangThai);
            return View(accountModels);
        }

        [HttpPost]
        public ActionResult CreateAccount(AdminAccountModels adminAccountModels)
        {
            var registerEmail = adminAccountModels.Email.Trim();
            var registerPassword = adminAccountModels.MatKhau.Trim();
            var registerRePassword = adminAccountModels.reMatKhau.Trim();

            currentRole = (int)Session["AccountType"];
            if (currentRole == 2)
            {
                ViewBag.Message = "Không thể tạo mới tài khoản với tài khoản quản trị";
                return View();
            }

            if (registerPassword.Equals(registerRePassword))
            {
                using (db)
                {
                    var checkAccount = db.TAIKHOANADMINs.Where(acc => acc.Email.Equals(registerEmail.Trim())).FirstOrDefault();
                    {
                        if (checkAccount != null)
                        {
                            ViewBag.Message = registerEmail + " tài khoản đã tồn tại";
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
                                newAccount.Quyen = adminAccountModels.Quyen;
                                newAccount.TrangThai = adminAccountModels.TrangThai;

                                db.TAIKHOANADMINs.Add(newAccount);
                                db.SaveChanges();
                                ViewBag.Message = newAccount.Ten + " tài khoản được tạo thành công";
                                //return RedirectToAction("Login");
                                return RedirectToAction("Index");
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
        public ActionResult Edit(int id, AdminAccountModels adminAccountModels)
        {
            if (Session["AccountID"] == null)
            {
                return RedirectToAction("Login", "Account", new { area = " " });
            }

            adminAccountModels = db.TAIKHOANADMINs.Where(acc => acc.ID.Equals(id)).Select(acc => new AdminAccountModels()
            {
                ID = acc.ID,
                Ten = acc.Ten,
                Email = acc.Email,
                Quyen = acc.Quyen,
                sTrangThai = acc.TRANGTHAI1.TenTrangThai,
                Sdt = acc.Sdt,
                PhongBan = acc.PhongBan,
                MatKhau = acc.MatKhau
            }).SingleOrDefault();

            adminAccountModels.RoleList = new SelectList(db.QUYENs, "IDQuyen", "TenQuyen", adminAccountModels.Quyen);
            adminAccountModels.StatusList = new SelectList(db.TRANGTHAIs, "IDTrangThai", "TenTrangThai", adminAccountModels.TrangThai);
            return View(adminAccountModels);
        }

        [HttpPost]
        public ActionResult Edit(AdminAccountModels adminAccountModels)
        {
            if (Session["AccountID"] == null)
            {
                return RedirectToAction("Login", "Account", new { area = " " });
            }

            //currentRole = (int)Session["AccountType"];
            //if (currentRole == 2)
            //{
            //    ViewBag.Message = "Không thể sửa với tài khoản quản trị";
            //    return View();
            //}

            var id = adminAccountModels.ID;
            using (db)
            {
                var accountdata = db.TAIKHOANADMINs.Where(acc => acc.ID.Equals(id)).FirstOrDefault();
                {
                    if (accountdata != null)
                    {
                        try
                        {
                            accountdata.Ten = adminAccountModels.Ten;
                            accountdata.Email = adminAccountModels.Email;
                            accountdata.Sdt = adminAccountModels.Sdt;
                            accountdata.PhongBan = adminAccountModels.PhongBan;
                            accountdata.Quyen = adminAccountModels.Quyen;
                            accountdata.TrangThai = adminAccountModels.TrangThai;
                            db.SaveChanges();
                            return RedirectToAction("Index", "Admin", new { area = " " });
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    }
                }
            }

            return RedirectToAction("Index", "Admin", new { area = " " });
        }

        public ActionResult Delete(int id)
        {
            var memberAccount = db.TAIKHOANADMINs.Where(t => t.ID.Equals(id)).FirstOrDefault();
            currentRole = (int)Session["AccountType"];

            if (memberAccount != null && currentRole != 2)
            {
                db.TAIKHOANADMINs.Remove(memberAccount);
                db.SaveChanges();
            }
            else if(currentRole == 2)
            {
                ViewBag.Message = "Không thể xóa với tài khoản quản trị";
            }
            return RedirectToAction("Index", "Admin", new { area = " " });
        }

        #region support method
        //private bool CheckLogin()
        //{
        //    if (Session["AccountID"] == null)
        //    {
        //        return false;
        //    }

        //    return true;
        //}
        #endregion

    }
}