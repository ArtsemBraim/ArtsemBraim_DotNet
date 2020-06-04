using System;

namespace Clinic.BLL.Dto
{
    public class Reception
    {
        public int DoctorId { get; set; }

        public int PatientId { get; set; }

        public Patient Patient { get; set; }

        public DateTime ReceptionTime { get; set; }
    }
}
