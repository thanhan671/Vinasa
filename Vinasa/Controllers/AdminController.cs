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
        public ActionResult ManageAccount()
        {
            return View(_db.TAIKHOANADMINs.OrderByDescending(it => it.ID).ToList());
        }

        [HttpGet]
        public ActionResult CreateAccount()
        {
            var accountModels = new TAIKHOANADMIN();
            accountModels.RoleList = new SelectList(_db.QUYENs, "IDQuyen", "TenQuyen", accountModels.Quyen);
            accountModels.StatusList = new SelectList(_db.TRANGTHAIs, "IDTrangThai", "TenTrangThai", accountModels.TrangThai);
            return View(accountModels);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateAccount([Bind(Include = "ID, Ten, Email, Quyen, TrangThai, Sdt, PhongBan, MatKhau, ChucDanh, matKhauMoi, xacNhanMatKhau")] TAIKHOANADMIN adminAccount)
        {
            if (adminAccount.xacNhanMatKhau.Equals(adminAccount.MatKhau))
            {
                _db.TAIKHOANADMINs.Add(adminAccount);
                _db.SaveChanges();
                return RedirectToAction("ManageAccount");
            }
            else
            {
                var accountModels = new TAIKHOANADMIN();
                accountModels.RoleList = new SelectList(_db.QUYENs, "IDQuyen", "TenQuyen", accountModels.Quyen);
                accountModels.StatusList = new SelectList(_db.TRANGTHAIs, "IDTrangThai", "TenTrangThai", accountModels.TrangThai);
                return View(accountModels);
            }

            return RedirectToAction("ManageAccount", "Admin", new { area = " " });
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

            if (memberAccount != null)
            {
                _db.TAIKHOANADMINs.Remove(memberAccount);
                _db.SaveChanges();
            }
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