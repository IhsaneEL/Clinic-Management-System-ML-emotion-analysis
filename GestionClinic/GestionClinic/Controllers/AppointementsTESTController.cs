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
    public class AppointementsTESTController : Controller
    {
        private BD_GCEntities db = new BD_GCEntities();

        // GET: AppointementsTEST
        public ActionResult Index()
        {
            var appointement = db.Appointement.Include(a => a.Doctor).Include(a => a.Patient);
            return View(appointement.ToList());
        }

        // GET: AppointementsTEST/Details/5
        public ActionResult Details(int? id)
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

        // GET: AppointementsTEST/Create
        public ActionResult Create()
        {
            ViewBag.doctor_ID = new SelectList(db.Doctor, "D_ID", "FullName");
            ViewBag.patient_ID = new SelectList(db.Patient, "P_ID", "Name");
            return View();
        }

        // POST: AppointementsTEST/Create
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Ap_ID,Date,patient_ID,doctor_ID,Time")] Appointement appointement)
        {
            if (ModelState.IsValid)
            {
                db.Appointement.Add(appointement);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.doctor_ID = new SelectList(db.Doctor, "D_ID", "FullName", appointement.doctor_ID);
            ViewBag.patient_ID = new SelectList(db.Patient, "P_ID", "Name", appointement.patient_ID);
            return View(appointement);
        }

        // GET: AppointementsTEST/Edit/5
        public ActionResult Edit(int? id)
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
            ViewBag.doctor_ID = new SelectList(db.Doctor, "D_ID", "FullName", appointement.doctor_ID);
            ViewBag.patient_ID = new SelectList(db.Patient, "P_ID", "Name", appointement.patient_ID);
            return View(appointement);
        }

        // POST: AppointementsTEST/Edit/5
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Ap_ID,Date,patient_ID,doctor_ID,Time")] Appointement appointement)
        {
            if (ModelState.IsValid)
            {
                db.Entry(appointement).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.doctor_ID = new SelectList(db.Doctor, "D_ID", "FullName", appointement.doctor_ID);
            ViewBag.patient_ID = new SelectList(db.Patient, "P_ID", "Name", appointement.patient_ID);
            return View(appointement);
        }

        // GET: AppointementsTEST/Delete/5
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

        // POST: AppointementsTEST/Delete/5
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
