using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages.Html;
using DocumentFormat.OpenXml.Wordprocessing;
using NexusMD.Data;
using NexusMD.Models.Appointment;
using NexusMD.Services;

namespace NexusMD.MVC.Controllers
{
    public class AppointmentsController : Controller
    {
        private readonly AppointmentService db = new AppointmentService();

        private readonly ApplicationDbContext _db = new ApplicationDbContext();

        public ActionResult Index(int? id)
        {
            var model = db.GetAllAppointments((int)id);

            return View(model);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var viewModel = db.GetAppointmentById((int)id);
          
            if (viewModel == null)
            {
                return HttpNotFound();
            }
            return View(viewModel);
        }

        public ActionResult Create()
        {
            AppointmentCreate model = new AppointmentCreate
            {
                PatientList = new SelectList(_db.Patients, "PatientId"),
                DoctorList = new SelectList(_db.Doctors, "DoctorId")
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Appointment appointment)
        {
            AppointmentCreate model = new AppointmentCreate
            {
                PatientList = new SelectList(_db.Patients, "PatientId"),
                DoctorList = new SelectList(_db.Doctors, "DoctorId")
            };

            return View(model);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var viewModel = db.GetAppointmentById((int)id);
                return View(viewModel);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            if (db.DeleteAppointment(id))
                return RedirectToAction("Index");

            return View();
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var detail = db.GetAppointmentById((int)id);

            if (detail is null)
                return HttpNotFound();

            var viewModel = new AppointmentEdit
            {
                AppointmentId = detail.AppointmentId,
                StartDateTime = detail.StartDateTime,
                Status = detail.Status,
                Notes = detail.Notes,
                Confirmation = detail.Confirmation
            };

            return View(viewModel);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AppointmentEdit model)
        {
            if (ModelState.IsValid)
            {
                if (db.UpdateAppointment(model))
                    return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}
