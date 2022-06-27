using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vinasa.Models;

namespace Vinasa.Controllers
{
    public class AdminController : Controller
    {
        #region global variable
        SEP25Team16Entities1 db = new SEP25Team16Entities1();

        private int accountID;
        #endregion

        // GET: Admin
        [HttpGet]
        public ActionResult Index()
        {
            var data = db.TAIKHOANADMINs.ToList();
            ViewBag.adminDetails = data;
            return View();
        }

        [HttpPost]
        public ActionResult Index(int accountID)
        {

            return View();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            AdminAccountModels adminAccountModels = db.TAIKHOANADMINs.Where(acc => acc.ID.Equals(id)).Select(acc => new AdminAccountModels()
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

            return View(adminAccountModels);
        }

        [HttpPost]
        public ActionResult Edit(AdminAccountModels adminAccountModels)
        {
            var name = adminAccountModels.Ten.Trim();
            var email = adminAccountModels.Email.Trim();
            var role = adminAccountModels.Quyen;
            var status = adminAccountModels.TrangThai;
            var phoneNumber = adminAccountModels.Sdt;
            var department = adminAccountModels.PhongBan.Trim();

            return Content(adminAccountModels.ID.ToString());
        }

        #region support method
        private int FindRowID()
        {

            return accountID;
        }
        #endregion
    }
}