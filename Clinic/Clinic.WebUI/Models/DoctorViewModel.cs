using Clinic.BLL.Dto;
using System.Collections.Generic;

namespace Clinic.WebUI.Models
{
    public class DoctorViewModel
    {
        public int Id { get; set; }

        public string Surname { get; set; }

        public string Name { get; set; }

        public string MiddleName { get; set; }

        public string ConsultingRoom { get; set; }

        public List<Reception> Receptions { get; set; }
    }
}
