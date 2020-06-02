using System;
using System.Collections.Generic;
using System.Text;

namespace Clinic.BLL.Dto
{
    public class PatientDto
    {
        public int Id { get; set; }

        public string Surname { get; set; }

        public string Name { get; set; }

        public string MiddleName { get; set; }
    }
}
