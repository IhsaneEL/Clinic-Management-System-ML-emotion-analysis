using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GestionClinic.Controllers
{
    public class home1Controller : Controller
    {
        // GET: home1
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Index1()
        {
            return RedirectToAction("AdminLogin","Admins");
        }
        public ActionResult Index2()
        {
            return RedirectToAction("PatientLogin", "Patients");
        }
    }
}