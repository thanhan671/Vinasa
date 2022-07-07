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

        #region main method
        // GET: Admin
        [HttpGet]
        public ActionResult Index()
        {
            if (Session["AccountID"] == null)
            {
                return RedirectToAction("Login", "Account", new { area = " " });
            }
            currentRole = (int)Session["AccountType"];
            if (currentRole == 2)
            {
                TempData["Message"] = "Lưu ý không thể xóa với tài khoản quản lí";
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
            CheckRole();

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
                return RedirectToAction("Index", "Admin", new { area = " " });
            }

            if (ModelState.IsValid)
            {
                if (registerPassword.Equals(registerRePassword))
                {
                    using (db)
                    {
                        var checkAccount = db.TAIKHOANADMINs.Where(acc => acc.Email.Equals(registerEmail.Trim())).FirstOrDefault();
                        {
                            if (checkAccount != null)
                            {
                                ViewBag.Message = registerEmail + " tài khoản đã tồn tại";

                                return RedirectToAction("CreateAccount");
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
            }
            return RedirectToAction("CreateAccount");
        }


        [HttpGet]
        public ActionResult Edit(int id, AdminAccountModels adminAccountModels)
        {
            if (Session["AccountID"] == null)
            {
                return RedirectToAction("Login", "Account", new { area = " " });
            }
            CheckRole();

            adminAccountModels = db.TAIKHOANADMINs.Where(acc => acc.ID.Equals(id)).Select(acc => new AdminAccountModels()
            {
                ID = acc.ID,
                Ten = acc.Ten,
                Email = acc.Email,
                Quyen = acc.Quyen,
                TrangThai = acc.TrangThai,
                Sdt = acc.Sdt,
                PhongBan = acc.PhongBan,
                MatKhau = acc.MatKhau
            }).SingleOrDefault();

            if(Session["AccountID"].Equals(id))
            {
                adminAccountModels.isEditMode = false;
            }    
            else
            {
                adminAccountModels.isEditMode = true;
            }
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

            currentRole = (int)Session["AccountType"];
            if (currentRole == 2)
            {
                return RedirectToAction("Index", "Admin", new { area = " " });
            }

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
            CheckRole();

            if (Session["AccountID"].Equals(id))
            {
                return View("Index");
            }

            if (memberAccount != null && currentRole != 2)
            {
                db.TAIKHOANADMINs.Remove(memberAccount);
                db.SaveChanges();
            }
            else if (currentRole == 2)
            {
                ViewBag.Message = "Không thể xóa với tài khoản quản lí";
            }
            return RedirectToAction("Index", "Admin", new { area = " " });
        }
        #endregion

        #region support method
        private void CheckRole()
        {
            currentRole = (int)Session["AccountType"];
            if (currentRole == 2)
            {
                TempData["Message"] = "Không thể thực hiện hành động này với tài khoản quản lí";
            }
        }
        #endregion

    }
}