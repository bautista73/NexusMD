using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NexusMD.Data;

namespace NexusMD.MVC.Controllers
{
    public class PatientProfilesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            var patientProfiles = db.PatientProfiles.Include(p => p.Appointment).Include(p => p.Doctor).Include(p => p.Patient).Include(p => p.Visit);
            return View(patientProfiles.ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PatientProfile patientProfile = db.PatientProfiles.Find(id);
            if (patientProfile == null)
            {
                return HttpNotFound();
            }
            return View(patientProfile);
        }

        // GET: PatientProfiles/Create
        public ActionResult Create()
        {
            ViewBag.AppointmentId = new SelectList(db.Appointments, "AppointmentId", "AppointmentStart");
            ViewBag.DoctorId = new SelectList(db.Doctors, "DoctorId", "FirstName");
            ViewBag.PatientId = new SelectList(db.Patients, "PatientId", "FirstName");
            ViewBag.VisitId = new SelectList(db.Visits, "VisitId", "Notes");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PatientProfileId,DoctorId,PatientId,AppointmentId,VisitId")] PatientProfile patientProfile)
        {
            if (ModelState.IsValid)
            {
                db.PatientProfiles.Add(patientProfile);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AppointmentId = new SelectList(db.Appointments, "AppointmentId", "AppointmentStart", patientProfile.AppointmentId);
            ViewBag.DoctorId = new SelectList(db.Doctors, "DoctorId", "FirstName", patientProfile.DoctorId);
            ViewBag.PatientId = new SelectList(db.Patients, "PatientId", "FirstName", patientProfile.PatientId);
            ViewBag.VisitId = new SelectList(db.Visits, "VisitId", "Notes", patientProfile.VisitId);
            return View(patientProfile);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PatientProfile patientProfile = db.PatientProfiles.Find(id);
            if (patientProfile == null)
            {
                return HttpNotFound();
            }
            ViewBag.AppointmentId = new SelectList(db.Appointments, "AppointmentId", "AppointmentStart", patientProfile.AppointmentId);
            ViewBag.DoctorId = new SelectList(db.Doctors, "DoctorId", "FirstName", patientProfile.DoctorId);
            ViewBag.PatientId = new SelectList(db.Patients, "PatientId", "FirstName", patientProfile.PatientId);
            ViewBag.VisitId = new SelectList(db.Visits, "VisitId", "Notes", patientProfile.VisitId);
            return View(patientProfile);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PatientProfileId,DoctorId,PatientId,AppointmentId,VisitId")] PatientProfile patientProfile)
        {
            if (ModelState.IsValid)
            {
                db.Entry(patientProfile).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AppointmentId = new SelectList(db.Appointments, "AppointmentId", "AppointmentStart", patientProfile.AppointmentId);
            ViewBag.DoctorId = new SelectList(db.Doctors, "DoctorId", "FirstName", patientProfile.DoctorId);
            ViewBag.PatientId = new SelectList(db.Patients, "PatientId", "FirstName", patientProfile.PatientId);
            ViewBag.VisitId = new SelectList(db.Visits, "VisitId", "Notes", patientProfile.VisitId);
            return View(patientProfile);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PatientProfile patientProfile = db.PatientProfiles.Find(id);
            if (patientProfile == null)
            {
                return HttpNotFound();
            }
            return View(patientProfile);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PatientProfile patientProfile = db.PatientProfiles.Find(id);
            db.PatientProfiles.Remove(patientProfile);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
