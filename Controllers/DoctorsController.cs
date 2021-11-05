using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NexusMD.Data;
using NexusMD.Models.Doctor;
using NexusMD.Services;

namespace NexusMD.MVC.Controllers
{
    [Authorize]
    public class DoctorController : Controller
    {
        public ActionResult Index()
        {
            var service = new DoctorService();
            var doctorModel = service.GetAllDoctors();
            return View(doctorModel);
        }

        public ActionResult Create()
        {
            var doctorModel = new DoctorCreate();
            return View(doctorModel);
        }

        public ActionResult Detail(int id)
        {
            var service = new DoctorService();
            var doctorModel = service.GetDoctorById(id);

            return View(doctorModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DoctorCreate doctorModel)
        {
            if (!ModelState.IsValid)
            {
                var service = new DoctorService();
                if (service.CreateDoctor(doctorModel))
                {
                    return RedirectToAction("Index");
                }
            }
            return View(doctorModel);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var service = new DoctorService();
            var doctorModel = service.GetDoctorById((int)id);
            return View(doctorModel);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteDoctorById(int id)
        {
            var service = new DoctorService();
            service.DeleteDoctor(id);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var service = new DoctorService();
            var edit = service.GetDoctorById((int)id);

            if (edit == null)
                return HttpNotFound();

            var doctorModel = new DoctorEdit()
            {
                DoctorId = edit.DoctorId,
                FirstName = edit.FirstName,
                LastName = edit.LastName,
                PhoneNumber = edit.PhoneNumber,
                Specialization = edit.Specialization
            };

            return View(doctorModel);
        }

        [HttpPost]
        [ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, DoctorEdit doctorModel)
        {
            if (ModelState.IsValid)
            {
                if (doctorModel.DoctorId != id)
                    return View(doctorModel);
            }
            var service = new DoctorService();

            if (service.UpdateDoctor(doctorModel))
            {
                return RedirectToAction("Index");
            }

            return View(doctorModel);
        }
    }
   
}
