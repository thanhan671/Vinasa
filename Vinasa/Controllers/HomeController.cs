﻿using System;
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
    }
}