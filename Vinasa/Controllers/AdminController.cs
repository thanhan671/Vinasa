using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vinasa.Models;
using System.Threading.Tasks;
using System.Data.Entity.Validation;
using System.Net;
using Vinasa.Session_Attribute;

namespace Vinasa.Controllers
{
    [SessionAttributes]
    public class AdminController : Controller
    {
        #region global variable
        private readonly SEP25Team16Entities2 _db = new SEP25Team16Entities2();
        int currentRole;
        #endregion

        #region main method
        // GET: Admin
        [HttpGet]
        public ActionResult ManageAccount()
        {
            currentRole = (int)Session["AccountType"];
            //if (currentRole == 2)
            //{
            //    TempData["Message"] = "Lưu ý không thể xóa với tài khoản quản lí";
            //}

            var data = _db.TAIKHOANADMINs.ToList();
            ViewBag.adminDetails = data;
            return View();
        }

        [HttpPost]
        public ActionResult ManageAccount(int accountID)
        {
            return View();
        }

        [HttpGet]
        public ActionResult CreateAccount()
        {
            CheckRole();

            AdminAccountModels accountModels = new AdminAccountModels();
            accountModels.RoleList = new SelectList(_db.QUYENs, "IDQuyen", "TenQuyen", accountModels.Quyen);
            accountModels.StatusList = new SelectList(_db.TRANGTHAIs, "IDTrangThai", "TenTrangThai", accountModels.TrangThai);
            return View(accountModels);
        }

        [HttpPost]
        public ActionResult CreateAccount(AdminAccountModels adminAccountModels)
        {
            var registerEmail = adminAccountModels.Email.Trim();
            var registerPassword = adminAccountModels.MatKhau.Trim();
            var registerRePassword = adminAccountModels.reMatKhau.Trim();

            if (!ModelState.IsValid)
            {
                if (registerPassword.Equals(registerRePassword))
                {
                    using (_db)
                    {
                        var checkAccount = _db.TAIKHOANADMINs.Where(acc => acc.Email.Equals(registerEmail.Trim())).FirstOrDefault();
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
                                    newAccount.ChucDanh = adminAccountModels.ChucDanh;

                                    _db.TAIKHOANADMINs.Add(newAccount);
                                    _db.SaveChanges();
                                    ViewBag.Message = newAccount.Ten + " tài khoản được tạo thành công";
                                    return RedirectToAction("ManageAccount");
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
        public ActionResult Edit(int? id)
        {
            CheckRole();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TAIKHOANADMIN adminAccount = _db.TAIKHOANADMINs.Find(id);
            if (adminAccount == null)
            {
                return HttpNotFound();
            }

            adminAccount.RoleList = new SelectList(_db.QUYENs, "IDQuyen", "TenQuyen", adminAccount.Quyen);
            adminAccount.StatusList = new SelectList(_db.TRANGTHAIs, "IDTrangThai", "TenTrangThai", adminAccount.TrangThai);

            return View(adminAccount);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID, Ten, Email, Quyen, TrangThai, Sdt, PhongBan, MatKhau, ChucDanh, matKhauMoi, xacNhanMatKhau")] TAIKHOANADMIN adminAccount)
        {
            if (!ModelState.IsValid)
            {
                adminAccount.RoleList = new SelectList(_db.QUYENs, "IDQuyen", "TenQuyen", adminAccount.Quyen);
                adminAccount.StatusList = new SelectList(_db.TRANGTHAIs, "IDTrangThai", "TenTrangThai", adminAccount.TrangThai);
                return View(adminAccount);
            }

            if (ModelState.IsValid)
            {
                if (adminAccount.matKhauMoi != null)
                {
                    if (adminAccount.matKhauMoi.Equals(adminAccount.xacNhanMatKhau))
                    {
                        adminAccount.MatKhau = adminAccount.matKhauMoi;
                        _db.Entry(adminAccount).State = System.Data.Entity.EntityState.Modified;
                        _db.SaveChanges();
                        return RedirectToAction("ManageAccount");
                    }
                }
                else
                {
                    _db.Entry(adminAccount).State = System.Data.Entity.EntityState.Modified;
                    _db.SaveChanges();
                    return RedirectToAction("ManageAccount");
                }
            }

            return RedirectToAction("ManageAccount", "Admin", new { area = " " });
        }

        public ActionResult Delete(int id)
        {
            var memberAccount = _db.TAIKHOANADMINs.Where(t => t.ID.Equals(id)).FirstOrDefault();
            CheckRole();

            if (Session["AccountID"].Equals(id))
            {
                return RedirectToAction("Login", "Account", new { area = " " });
            }

            if (memberAccount != null && currentRole != 2)
            {
                _db.TAIKHOANADMINs.Remove(memberAccount);
                _db.SaveChanges();
            }
            //else if (currentRole == 2)
            //{
            //    ViewBag.Message = "Không thể xóa với tài khoản quản lí";
            //}
            return RedirectToAction("ManageAccount", "Admin", new { area = " " });
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