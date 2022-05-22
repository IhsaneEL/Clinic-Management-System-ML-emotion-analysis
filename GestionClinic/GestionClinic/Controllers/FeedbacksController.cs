using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GestionClinic;
using GestionClinicML.Model;

namespace GestionClinic.Controllers
{
    public class FeedbacksController : Controller
    {
        private BD_GCEntities db = new BD_GCEntities();

        // GET: Feedbacks
        public ActionResult Index()
        {
            var feedback = db.Feedback.Include(f => f.Patient);
            return View(feedback.ToList());
        }

        // GET: Feedbacks/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Feedback feedback = db.Feedback.Find(id);
            if (feedback == null)
            {
                return HttpNotFound();
            }
            return View(feedback);
        }

        // GET: Feedbacks/Create
        public ActionResult Create()
        { 
            Feedback f = new Feedback();
            string feedback= TempData["userFeedback"].ToString();
            ModelInput sampleData = new ModelInput()
            {
                Col0 =feedback,
            };
            f.patient_ID = Convert.ToInt32(TempData["userId"].ToString());
            f.feedback1 = TempData["userFeedback"].ToString();
            f.feedback1 = sampleData.Col0;
            var result = ConsumeModel.Predict(sampleData);
            f.result = result.Prediction;
          
           // f.result = Convert.ToString(TempData["result"]);
            return RedirectToAction("Createfeed",f);
        }

        // POST: Feedbacks/Create
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
       // [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Createfeed([Bind(Include = "patient_ID,feedback1,result")] Feedback feedback)
        {
            //feedback.patient_ID = (int)Session["userId"];


            db.Feedback.Add(feedback);

            db.SaveChanges();
           
                return RedirectToAction("PatientHome","Patients");
           
        }
        // GET: Feedbacks/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Feedback feedback = db.Feedback.Find(id);
            if (feedback == null)
            {
                return HttpNotFound();
            }
            ViewBag.patient_ID = new SelectList(db.Patient, "P_ID", "Name", feedback.patient_ID);
            return View(feedback);
        }

        // POST: Feedbacks/Edit/5
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "patient_ID,feedback1")] Feedback feedback)
        {
            if (ModelState.IsValid)
            {
                db.Entry(feedback).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.patient_ID = new SelectList(db.Patient, "P_ID", "Name", feedback.patient_ID);
            return View(feedback);
        }

        // GET: Feedbacks/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Feedback feedback = db.Feedback.Find(id);
            if (feedback == null)
            {
                return HttpNotFound();
            }
            return View(feedback);
        }

        // POST: Feedbacks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Feedback feedback = db.Feedback.Find(id);
            db.Feedback.Remove(feedback);
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
