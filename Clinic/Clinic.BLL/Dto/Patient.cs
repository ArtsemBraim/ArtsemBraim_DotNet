using Clinic.DAL.Domain;
using System.Collections.Generic;

namespace Clinic.BLL.Dto
{
    public class Patient
    {
        public int Id { get; set; }

        public string Surname { get; set; }

        public string Name { get; set; }

        public string MiddleName { get; set; }

        public List<Reception> Receptions { get; set; }
    }
}
