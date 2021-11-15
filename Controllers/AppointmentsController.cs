using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NexusMD.Data;
using NexusMD.Models.Appointment;
using NexusMD.Services;

namespace NexusMD.MVC.Controllers
{
    public class AppointmentsController : Controller
    {
        private readonly AppointmentService db = new AppointmentService();

        public ActionResult Index()
        {
            var viewModel = db.GetAllAppointments();
            return View(viewModel);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
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
            var viewModel = new AppointmentCreate();
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AppointmentCreate model)
        {
            if (ModelState.IsValid)
            {
                if (db.CreateAppointment(model))
                {
                    TempData["SaveResult"] = "A new appointment was created.";
                    return RedirectToAction("Index");
                }
            }
            return View(model);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);

            var viewModel = db.GetAppointmentById((int)id);

            if (viewModel is null)
                return HttpNotFound();

            return View(viewModel);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            if (db.DeleteAppointment(id))
            {
                TempData["SaveResult"] = "An appointment was deleted.";
                return RedirectToAction("Index");
            }


            ModelState.AddModelError("", "Appointment could not be deleted.");

            return View();
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);

            var detail = db.GetAppointmentById((int)id);

            if (detail is null)
                return HttpNotFound();

            var viewModel = new AppointmentEdit
            {
                AppointmentId = detail.AppointmentId,
                StartDateTime = detail.StartDateTime,
                DoctorId = detail.DoctorId,
                Status = detail.Status,
                Notes = detail.Notes,
                Confirmation = detail.Confirmation
            };

            return View(viewModel);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, AppointmentEdit model)
        {
            if (ModelState.IsValid)
            {
                if (model.AppointmentId != id)
                {
                    ModelState.AddModelError("", "ID Mismatch");
                    return View(model);
                }

                if (db.UpdateAppointment(model))
                {
                    TempData["SaveResult"] = "An Appointment was updated";
                    return RedirectToAction("Index");
                }
            }

            ModelState.AddModelError("", "Appointment could not be updated");
            return View(model);
        }
    }
}
