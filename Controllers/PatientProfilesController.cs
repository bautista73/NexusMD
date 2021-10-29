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

        // GET: PatientProfiles
        public ActionResult Index()
        {
            var patientProfiles = db.PatientProfiles.Include(p => p.Appointment).Include(p => p.Doctor).Include(p => p.Patient);
            return View(patientProfiles.ToList());
        }

        // GET: PatientProfiles/Details/5
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
            ViewBag.AppointmentId = new SelectList(db.Appointments, "AppointmentId", "Notes");
            ViewBag.DoctorId = new SelectList(db.Doctors, "DoctorId", "FirstName");
            ViewBag.PatientId = new SelectList(db.Patients, "PatientId", "FirstName");
            return View();
        }

        // POST: PatientProfiles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PatientProfileId,DoctorId,PatientId,AppointmentId")] PatientProfile patientProfile)
        {
            if (ModelState.IsValid)
            {
                db.PatientProfiles.Add(patientProfile);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AppointmentId = new SelectList(db.Appointments, "AppointmentId", "Notes", patientProfile.AppointmentId);
            ViewBag.DoctorId = new SelectList(db.Doctors, "DoctorId", "FirstName", patientProfile.DoctorId);
            ViewBag.PatientId = new SelectList(db.Patients, "PatientId", "FirstName", patientProfile.PatientId);
            return View(patientProfile);
        }

        // GET: PatientProfiles/Edit/5
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
            ViewBag.AppointmentId = new SelectList(db.Appointments, "AppointmentId", "Notes", patientProfile.AppointmentId);
            ViewBag.DoctorId = new SelectList(db.Doctors, "DoctorId", "FirstName", patientProfile.DoctorId);
            ViewBag.PatientId = new SelectList(db.Patients, "PatientId", "FirstName", patientProfile.PatientId);
            return View(patientProfile);
        }

        // POST: PatientProfiles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PatientProfileId,DoctorId,PatientId,AppointmentId")] PatientProfile patientProfile)
        {
            if (ModelState.IsValid)
            {
                db.Entry(patientProfile).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AppointmentId = new SelectList(db.Appointments, "AppointmentId", "Notes", patientProfile.AppointmentId);
            ViewBag.DoctorId = new SelectList(db.Doctors, "DoctorId", "FirstName", patientProfile.DoctorId);
            ViewBag.PatientId = new SelectList(db.Patients, "PatientId", "FirstName", patientProfile.PatientId);
            return View(patientProfile);
        }

        // GET: PatientProfiles/Delete/5
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

        // POST: PatientProfiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PatientProfile patientProfile = db.PatientProfiles.Find(id);
            db.PatientProfiles.Remove(patientProfile);
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
