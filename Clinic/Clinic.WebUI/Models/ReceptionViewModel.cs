using Clinic.BLL.Dto;
using System;

namespace Clinic.WebUI.Models
{
    public class ReceptionViewModel
    {
        public int DoctorId { get; set; }

        public int PatientId { get; set; }

        public Patient Patient { get; set; }

        public DateTime ReceptionTime { get; set; }
    }
}
