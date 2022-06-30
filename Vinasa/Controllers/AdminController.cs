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
        public ActionResult Edit(int id, AdminAccountModels adminAccountModels)
        {
            if (Session["AccountID"] == null)
            {
                return RedirectToAction("Login", "Account", new { area = " " });
            }
            //ViewBag.RoleList = 

            adminAccountModels = db.TAIKHOANADMINs.Where(acc => acc.ID.Equals(id)).Select(acc => new AdminAccountModels()
            {
                ID = acc.ID,
                Ten = acc.Ten,
                Email = acc.Email,
                sQuyen = acc.QUYEN1.TenQuyen,
                sTrangThai = acc.TRANGTHAI1.TenTrangThai,
                Sdt = acc.Sdt,
                PhongBan = acc.PhongBan,
                MatKhau = acc.MatKhau
            }).SingleOrDefault();

            return View(adminAccountModels);
        }

        [HttpPost]
        public ActionResult Edit(AdminAccountModels adminAccountModels)
        {
            if (Session["AccountID"] == null)
            {
                return RedirectToAction("Login", "Account", new { area = " " });
            }
            var id = adminAccountModels.ID;
            //string sRole, sStatus;
            //using (db)
            //{
            //    var checkRole = db.QUYENs.Where(q => q.IDQuyen.Equals(adminAccountModels.Quyen)).FirstOrDefault();
            //    var checkStatus = db.TRANGTHAIs.Where(t => t.IDTrangThai.Equals(adminAccountModels.TrangThai)).FirstOrDefault();
            //    sRole = checkRole.TenQuyen;
            //    sStatus = checkStatus.TenTrangThai;
            //}
            //var role = sRole;
            //var status = sStatus;
            //var role = adminAccountModels.Quyen;
            //var status = adminAccountModels.sTrangThai;

            //if(ModelState.IsValid)
            //{
            //    return View(adminAccountModels);
            //}
            //else
            //{
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

            return Content(id.ToString());
        }

        public ActionResult Delete(int id)
        {
            var memberAccount = db.TAIKHOANADMINs.Where(t => t.ID.Equals(id)).FirstOrDefault();
            if (memberAccount != null)
            {
                db.TAIKHOANADMINs.Remove(memberAccount);
                db.SaveChanges();
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