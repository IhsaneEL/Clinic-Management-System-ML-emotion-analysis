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
    public class AppointementsController : Controller
    {
        private BD_GCEntities db = new BD_GCEntities();

        // GET: Appointements
        public ActionResult Index()
        {
    
            return View(db.Appointement.ToList());
        }

        // GET: Appointements/Details/5
        public ActionResult ViewAppointment(int? id)
        {
            if (id == null)
            {
                return View();
            }
            List<Appointement> appointement = db.Appointement.Where(x=>x.patient_ID==id).ToList();
            if (appointement == null)
            {
                return View(appointement);
            }
            return View(appointement);
        }

        // GET: Appointements/Create
        public ActionResult CreateAppointment(int ? id)
        {
            ViewBag.doctor_ID = new SelectList(db.Doctor, "D_ID", "FullName");
            ViewBag.patient_ID = id.ToString();
            return View();
        }

        // POST: Appointements/Create
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddAppointement([Bind(Include = "Ap_ID,Date,Time,patient_ID,doctor_ID")] Appointement appointement)
        {
            appointement.patient_ID = (int)Session["userId"];
            if (ModelState.IsValid)
            {
                db.Appointement.Add(appointement);
                db.SaveChanges();
                return View();
            }

           // ViewBag.doctor_ID = new SelectList(db.Doctor, "D_ID", "FullName", appointement.doctor_ID);
           // ViewBag.patient_ID = Session["userId"].ToString();
            return View("appointement failed");
        }

     

        // GET: Appointements/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appointement appointement = db.Appointement.Find(id);
            if (appointement == null)
            {
                return HttpNotFound();
            }
            return View(appointement);
        }

        // POST: Appointements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Appointement appointement = db.Appointement.Find(id);
            db.Appointement.Remove(appointement);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
