using Clinic.DAL.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Clinic.BLL.Dto
{
    public class DoctorDto
    {
        public int Id { get; set; }

        public string Surname { get; set; }

        public string Name { get; set; }

        public string MiddleName { get; set; }

        public string ConsultingRoom { get; set; }

        public Specialization Specialization { get; set; }
    }
}
