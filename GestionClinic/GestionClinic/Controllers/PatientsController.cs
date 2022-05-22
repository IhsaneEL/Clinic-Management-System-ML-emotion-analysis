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
    public class PatientsController : Controller
    {
        private BD_GCEntities db = new BD_GCEntities();

        // GET: Patients login
        public ActionResult PatientLogin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult PatientLogVerif(Patient patient)
        {
            if (ModelState.IsValid)
            {
                var result = db.Patient.Where(model => model.UserName == patient.UserName && model.Password == patient.Password).FirstOrDefault();


                if (result != null)
                {
                    Session["userId"] = result.P_ID;
                    return RedirectToAction("PatientHome", "Patients");
                }
                else
                {
                    ViewBag.Message = string.Format("Username and/or Password incorrect");
                    return RedirectToAction("PatientLogin", "Patients");
                }
            }

            return RedirectToAction("PatientLogin", "Patients");

        }
        // GET: Patients
        public ActionResult PatientReg()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddPatient([Bind(Include = "P_ID,Name,LastName,Address,ContactNumber,Email,UserName,Password")] Patient patient)
        {
            if (ModelState.IsValid)
            {
                db.Patient.Add(patient);
                db.SaveChanges();
                return RedirectToAction("PatientLogin", "Patients");
            }

            return View("PatientLogin", "Patients");
        }

        public ActionResult PatientHome()
        {
            int id = Convert.ToInt32(Session["userId"].ToString());
            var patient = db.Patient.Where(x => x.P_ID == id).FirstOrDefault();
            // Patient patient = db.Patient.Find(id);
            if (patient == null)
            {
                return View();
            }
            return View(patient);

        }
        public ActionResult ViewPatients()
        {
            return View(db.Patient.ToList());
        }
        public ActionResult SendFeedback(string feedback)
        {
           // ModelInput m = new ModelInput();
            //m.Col0 = feedback;
            TempData["userId"] = Session["userId"];
            TempData["userFeedback"] = feedback;
            //var result = ConsumeModel.Predict(feedback);
            //TempData["result"] = result;
            return RedirectToAction("Create", "Feedbacks");
        }

        public ActionResult PartialView()
        {
            return View();
        }
        public PartialViewResult Feedback()
        {
            ModelInput feedback = new ModelInput();
            return PartialView(feedback);
        }
        public PartialViewResult PatientDetails()
        {
            int id = Convert.ToInt32(Session["userId"].ToString());
            var patient = db.Patient.Where(x => x.P_ID == id).FirstOrDefault();
            // Patient patient = db.Patient.Find(id);
            if (patient == null)
            {
                return PartialView(patient);
            }
            return PartialView(patient);


            /*
            public ActionResult Index()
            {
                return View(db.Patient.ToList());
            }



            // GET: Patients/Create
            public ActionResult Create()
            {
                return View();
            }

            // POST: Patients/Create
            // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
            // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.

            // GET: Patients/Details/5
            public ActionResult PatientHome(int? id)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Patient patient = db.Patient.Find(id);
                if (patient == null)
                {
                    return HttpNotFound();
                }
                return View(patient);
            }


            // GET: Patients/Edit/5
            public ActionResult Edit(int? id)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Patient patient = db.Patient.Find(id);
                if (patient == null)
                {
                    return HttpNotFound();
                }
                return View(patient);
            }

            // POST: Patients/Edit/5
            // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
            // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
            [HttpPost]
            [ValidateAntiForgeryToken]
            public ActionResult Edit([Bind(Include = "P_ID,Name,LastName,Address,ContactNumber,Email,UserName,Password")] Patient patient)
            {
                if (ModelState.IsValid)
                {
                    db.Entry(patient).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(patient);
            }

            // GET: Patients/Delete/5
            public ActionResult Delete(int? id)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Patient patient = db.Patient.Find(id);
                if (patient == null)
                {
                    return HttpNotFound();
                }
                return View(patient);
            }

            // POST: Patients/Delete/5
            [HttpPost, ActionName("Delete")]
            [ValidateAntiForgeryToken]
            public ActionResult DeleteConfirmed(int id)
            {
                Patient patient = db.Patient.Find(id);
                db.Patient.Remove(patient);
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
            */
        }
    }
}
