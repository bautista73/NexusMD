using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NexusMD.MVC.Models
{
    public class AppointmentViewModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Appointment Number")]
        public int AppointmentId { get; set; }

        [Required(ErrorMessage = "Please select a patient")]
        public int PatientId { get; set; }
        public IEnumerable<SelectListItem> Patient { get; set; }


        [Required(ErrorMessage = "Please select a doctor")]
        public int DoctorId { get; set; }
        public IEnumerable<SelectListItem> Doctor { get; set; }

        public string Status { get; set; }


        [Display(Name = "Scheduled Time")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime StartDateTime { get; set; }
        public string Notes { get; set; }
        public bool Confirmation { get; set; }
    }
}