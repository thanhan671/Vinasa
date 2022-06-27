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
        SEP25Team16Entities1 db = new SEP25Team16Entities1();
        // GET: Admin
        [HttpGet]
        public ActionResult Index()
        {
            
            return View(db.TAIKHOANADMINs.ToList());
        }
        public ActionResult Edit()
        {
            return View();
        }
    }
}