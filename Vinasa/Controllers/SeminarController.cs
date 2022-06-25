using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Vinasa.Controllers
{
    public class SeminarController : Controller
    {
        // GET: Seminar
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ViewList()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }
        public ActionResult UpdateInfoS()
        {
            return View();

        }
        public ActionResult UpdateInfoM()
        {
            return View();

        }

        public ActionResult DetailInfoSoM()
        {
            return View();
        }

        public ActionResult DetailInfoS()
        {
            return View();
        }
    }
}