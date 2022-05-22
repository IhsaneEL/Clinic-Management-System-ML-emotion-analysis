using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GestionClinic;

namespace GestionClinic.Controllers
{
    public class AdminsController : Controller
    {
        private BD_GCEntities db = new BD_GCEntities();

        public ActionResult AdminLogin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AdminLogVerif(Admin admin)
        {
            if (ModelState.IsValid)
            {
                var result = db.Admin.Where(model => model.username == admin.username && model.password == admin.password).FirstOrDefault();
                if (result != null)
                {
                    return RedirectToAction("Index", "Feedbacks");
                }
                else
                {
                    ViewBag.Message = string.Format("Username and/or Password incorrect");
                    return RedirectToAction("AdminLogin","Admins");
                }
            }
            return RedirectToAction("AdminLogin", "Admins");

        }
        public ActionResult AdminHome()
        {

            return View();
        }
    }
}