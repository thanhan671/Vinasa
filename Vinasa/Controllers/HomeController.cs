using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Vinasa.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (Session["AccountID"] != null)
            {
                return View();
            }

            return RedirectToAction("Login", "Account", new { area = " " });
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}