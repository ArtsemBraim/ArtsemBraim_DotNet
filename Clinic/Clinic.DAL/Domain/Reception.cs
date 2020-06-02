using System;

namespace Clinic.DAL.Domain
{
    public class Reception : Entity
    {
        public int DoctorId { get; set; }

        public int PatientId { get; set; }

        public DateTime ReceptionTime { get; set; }
    }
}
