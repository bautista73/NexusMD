using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages.Html;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using NexusMD.Data;
using NexusMD.Models.Appointment;
using NexusMD.MVC.Models;
using NexusMD.Services;
using SelectListItem = System.Web.Mvc.SelectListItem;

namespace NexusMD.MVC.Controllers
{
    public class AppointmentsController : Controller
    {
        private readonly ApplicationDbContext _db = new ApplicationDbContext();

        public ActionResult Index()
        {
            var appointments = _db.Appointments.Include("Doctor").Include("Patient");
            return View(appointments);
        }

        public ActionResult Details(int id)
        {
            Appointment appointment = _db.Appointments.Where(x => x.AppointmentId == id).SingleOrDefault();
            return View(appointment);
        }

        public ActionResult Create()
        {
            var p = _db.Patients.Select(r => r.FirstName);

            var d = _db.Doctors.Select(r => r.FirstName);

            var viewModel = new AppointmentViewModel
            {
                Patient = new SelectList(p),

                Doctor = new SelectList(d)
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Appointment appointment)
        {
            var p = _db.Patients.Select(r => r.FirstName);

            var d = _db.Doctors.Select(r => r.FirstName);

            var viewModel = new AppointmentViewModel
            {
                Patient = new SelectList(p),

                Doctor = new SelectList(d)
            };

            _db.Appointments.Add(appointment);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            Appointment appointment = _db.Appointments.Where(x => x.AppointmentId == id).SingleOrDefault();
            return View(appointment);
        }

        [HttpPost]
        public ActionResult Edit(Appointment model)
        {
            Appointment appointment = _db.Appointments.Where(x => x.AppointmentId == model.AppointmentId).SingleOrDefault();
            if(appointment != null)
            {
                _db.Entry(appointment).CurrentValues.SetValues(appointment);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public ActionResult Delete(int id)
        {
            Appointment appointment = _db.Appointments.Find(id);
            return View(appointment);
        }


        [HttpPost]
        public ActionResult Delete(int id, Appointment model)
        {
            var appointment = _db.Appointments.Where(x => x.AppointmentId == id).SingleOrDefault();
            if (appointment != null)
            {
                _db.Appointments.Remove(appointment);
                _db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
