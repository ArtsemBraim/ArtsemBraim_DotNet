using Clinic.DAL.Interfaces;

namespace Clinic.DAL.Domain
{
    public class Doctor : IEntity
    {
        public int Id { get; set; }

        public string Surname { get; set; }

        public string Name { get; set; }

        public string MiddleName { get; set; }

        public string ConsultingRoom { get; set; }

        public Specialization Specialization { get; set; }
    }
}
